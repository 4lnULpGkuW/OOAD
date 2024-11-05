using System;
using System.Collections.Generic;

public class Club
{
    public string name { get; set; }    
    public List<Member> memberList { get; set; }
    public List<Staff> staffList { get; set; }
    public void createService(params object[] args) {}
    public void createBonus(params object[] args) {}
}

public class Person
{
    public string name { get; set; }
    public Club club { get; set; }
    public abstract void joinClub(Club club){}
}

public class Member : Person
{
    public Bonus bonus { get; set; }
    public List<Visit> visitHistory { get; set; }
    public override void joinClub(Club club) {}

    public void makeVisit(Club club)
    {
        Visit visit = new Visit();
        visitHistory.Add(visit);
    }

    public void orderService(Service service)
    {
        visitHistory.Last().createAOS(this, service);
    }
}

public class Staff : Person
{
    public string position { get; set; }
    public bool isFree { get; set; }
    public override void joinClub(Club club) {}
    public void provideService(ActOfService aos) {}
}

public class Bonus
{
    public string name { get; set; }
    public int discount { get; set; }
}

public class Service
{
    public string name { get; set; }
    public int price { get; set; }
}

public class Visit
{
    public List<ActOfService> orderHistory { get; set; }
    public string date { get; set; }

    public void createAOS(Member member, Service service)
    {
        aos = new ActOfService(member, service);
        orderHistory.Add(aos);
    }
}

public class ActOfService
{
    public string datetime { get; set; }
    public int price { get; set; }
    public Staff staff { get; set; }
    public Service service { get; set; }

    public ActOfService(Member member, Service service)
    {
        datetime = DateTime.Now;
        this.service = service;
        if (bonus.discount != 0) 
        {
            price = service.price * (1 - member.bonus.discount);
        }
        else 
        {
            price = service.price;
        }
        assignStaff(member.club);
    }

    private void assignStaff(Club club)
    {
        foreach (Staff staff in club.staffList)
        {
            if (staff.isFree) 
            {
                this.staff = staff;
                staff.provideService(this);
            }
        }
    }
}
