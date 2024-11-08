using System;
using System.Collections.Generic;

public class Club
{
    public string name { get; set; }    
    public List<Member> memberList { get; set; }
    public List<Staff> serviceList { get; set; }
    public List<Staff> bonusList { get; set; }
    public void createService(params object[] args) {
        Service service = new service(args []);
        serviceList.Add(service);
    }
    public void createBonus(params object[] args) {
        Service bonus = new bonus(args []);
        bonusList.Add(bonus);
    }
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
        Visit visit = new Visit(this);
        visitHistory.Add(visit);
    }

    public void orderService(Service service)
    {
        visitHistory.Last().createAOS(service);
    }

    public void assignBonus(Bonus bonus) {}
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
    public Bonus(args []) {}
    public string name { get; set; }
    public int discount { get; set; }
}

public class Service
{
    public Service(args []) {}
    public string name { get; set; }
    public int price { get; set; }
}

public class Visit
{
    public List<ActOfService> orderHistory { get; set; }
    public string date { get; set; }
    public Member member { get; set; }
    public Visit(Member member) {}
    public void createAOS(Service service)
    {
        aos = new ActOfService(this, service);
        orderHistory.Add(aos);
    }
}

public class ActOfService
{
    public string datetime { get; set; }
    public int price { get; set; }
    public Visit visit { get; set; }
    public Staff staff { get; set; }
    public Service service { get; set; }

    public ActOfService(Visit visit, Service service)
    {
        datetime = DateTime.Now;
        this.visit = visit;
        this.service = service;
        if (visit.member.bonus.discount != 0) 
        {
            price = service.price * (1 - visit.member.bonus.discount);
        }
        else 
        {
            price = service.price;
        }
        assignStaff();
    }

    private void assignStaff()
    {
        foreach (Staff staff in visit.member.club.staffList)
        {
            if (staff.isFree)
            {
                this.staff = staff;
                staff.provideService(this);
            }
        }
    }
}
