using GIT.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIT;

public class Branch : Component
{
    #region propertys
    private string _name;
    private IState _status;
    private string _owner;
    private Stack<Branch> history;

    public Branch(string name, string owner) : base(name, owner)
    {
        Children = new();
        history = new();
    }

    public override string Name { get => _name; set => _name = value; }
    public override IState Status { get => _status; set => _status = value; }
    public override string Owner { get => _owner; set => _owner = value; }
    public Dictionary<string, Component> Children { get; set; }
    #endregion
    public void Clone(string name)
    {
        Branch branch = new(name, this.Owner)
        {
            _status = _status,
            Children = new()
        };
        foreach(var item in Children)
        {
            branch.Children.Add(item.Key, item.Value);
        }
        this.Children.Add(name, branch);
        Status = Draft.GetInstance();
    }
    public override void Print()
    {
        Console.WriteLine($" I'm branch {this._name} and my children are:");
        foreach (var child in Children)
        {
            child.Value.Print();
        }
    }
    public void Add(Component component)
    {
        Children.Add(component.Name, component);
        Status = Draft.GetInstance();
    }
    #region actions
    public void DeleteBranch(string name)
    {
        Children.Remove(name);
        Status = Draft.GetInstance();
    }

    public override void Merge(Component other)
    {
        if (this.Status.GetStatus() != "ReadyToMerge")
        {
            Console.WriteLine("you can not merge");
            return;
        }
        history.Push(new Branch(this.Name, this.Owner) { Status = this.Status, Children = this.Children });
        foreach (var child in ((Branch)other).Children)
        {
            try
            {
                this.Children[child.Key].Merge(child.Value);
            }
            catch (Exception)
            {
                this.Children.Add(child.Key, child.Value);
            }
        }
        Status.ChangeStatus(this);
    }

    public override void Undo()
    {
        Branch last = history.Pop();
        if (last == null) return;
        this.Status = last.Status;
        this.Children = last.Children;
        foreach (var child in this.Children)
        {
            child.Value.Undo();
        }
    }
    public Branch GetBranch(string name)
    {
        try
        {
            return (Branch)Children[name];
        }
        catch
        {
            Branch res;
            foreach (var branch in Children)
            {
                if(branch.GetType() != typeof(Branch)) { return null; }
                res = ((Branch)branch.Value).GetBranch(name);
                if (res != null)
                {
                    return res;
                }
            }
        }
        return null;
    }
    #endregion
}

