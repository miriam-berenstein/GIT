using GIT.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIT;

public abstract class Component
{
    public abstract string Name { get; set; }
    public abstract IState Status { get; set; }
    public abstract string Owner { get; set; }
    public Component(string name, string owner)
    {
        Name = name;
        Owner = owner;
        Status = Draft.GetInstance();
    }
    public abstract void Merge(Component other);
    public abstract void Undo();
    public abstract void Print();
    public void Add()
    {
        if (Status.GetStatus() == "Draft")
        {
            Status.ChangeStatus(this);
        }
    }
    public void Commit()
    {
        if (Status.GetStatus() == "Staged")
        {
            Status.ChangeStatus(this);
        }
    }
    public void Pull()
    {
        if (Status.GetStatus() == "Commited")
        {
            Status.ChangeStatus(this);
            Status.ChangeStatus(this);
        }
    }
    public void Push()
    {
        if (Status.GetStatus() == "Staged")
        {
            Status.ChangeStatus(this);
        }
    }
}
