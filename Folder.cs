using GIT.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIT;

public class Folder : Component
{
    private Stack<Folder> history;
    private string _name;
    private IState _status;
    private string _owner;

    public Folder(string name, string owner) : base(name, owner)
    {
        Children = new();
        history = new();
    }

    public override string Name { get => _name; set => _name = value; }
    public override IState Status { get => _status; set => _status = value; }
    public override string Owner { get => _owner; set => _owner = value; }
    public Dictionary<string, Component> Children { get; set; }

    public void Add(Component component)
    {
        if (component.GetType() == typeof(Branch)) return;
        Children.Add(component.Name, component);
        Status = Draft.GetInstance();
    }



    public override void Merge(Component other)
    {
        if (this.Status.GetStatus() != "ReadyToMerge")
        {
            Console.WriteLine("you can not merge");
            return;
        }
        history.Push(new Folder(this.Name, this.Owner) { Status = this.Status, Children = this.Children});
        foreach (var child in ((Folder)other).Children)
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

    public override void Print()
    {
        Console.WriteLine($"  I'm folder {this._name} and my children are:");
        foreach(var child in Children)
        {
            child.Value.Print();
        }
    }

    public override void Undo()
    {
        var last = history.Pop();
        if(last == null) return;
        this.Status = last.Status;
        this.Children = last.Children;
        foreach(var child in this.Children)
        {
            child.Value.Undo();
        }
    }
}
