using GIT.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIT;

public class MyFile : Component
{
    public MyFile(string name, string owner, string content = null) : base(name, owner)
    {
        history = new();
        this.Content= content;
    }

    private Stack<MyFile> history;
    private string _name;
    private IState _status;
    private string _owner;

    public override string Name { get => _name; set => _name = value; }
    public override IState Status { get => _status; set => _status = value; }
    public override string Owner { get => _owner; set => _owner = value; }
    public string Content { get; set; }
    
    public override void Merge(Component other)
    {
        if(this.Status.GetStatus() != "ReadyToMerge")
        {
            Console.WriteLine("you can not merge");
            return;
        }
        history.Push(new MyFile(this.Name, this.Owner) { Status = this.Status, Content = this.Content});
        this.Content = ((MyFile)other).Content;
        Status.ChangeStatus(this);
    }

    public override void Undo()
    {
        var last = history.Pop();
        if (last == null) return;
        this.Status = last.Status;
        this.Content = last.Content;
    }

    public override void Print()
    {
        Console.WriteLine($"   I'm file - {this._name} and my content is : {this.Content}");
    }
}
