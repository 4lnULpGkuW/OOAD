using System;
using System.Collections.Generic;

public class Club
{
    public string name { get; set; }    
    public List<Member> memberList { get; set; }
    public List<Service> serviceList { get; set; }
    public List<Bonus> bonusList { get; set; }
    public void CreateService(params String[] args) {
        Service service = new Service(args);
        serviceList.Add(service);
    }
    public void CreateBonus(params String[] args) {
        Service bonus = new Bonus(args);
        bonusList.Add(bonus);
    }
}

public class Person
{
    public string name { get; set; }
    public Club club { get; set; }
    public abstract void JoinClub(Club club){}
}

public class Member : Person
{
    public Bonus bonus { get; set; }
    public List<Visit> visitHistory { get; set; }
    public override void JoinClub(Club club) {
        this.club = club;
        club.memberList.Add(club);
    }

    public void MakeVisit(Club club)
    {
        Visit visit = new Visit(this);
        visitHistory.Add(visit);
    }

    public void OrderService(Service service)
    {
        visitHistory.Last().CreateAOS(service);
    }
}

public class Staff : Person
{
    public string position { get; set; }
    public bool isFree { get; set; }
    public override void JoinClub(Club club) {
        this.club = club;
        club.staffList.Add(club);
    }
    public void ProvideService(ActOfService aos) {}
}

public class Administrator : Person
{
    public override void JoinClub(Club club) {
        this.club = club;
    }
    public void AssignBonus(Member member, Bonus bonus) {}
    public void Administrate(String[] args) 
    {
        // ...
        if (args) 
        {
            club.CreateService(args);
        }
        // ...
        if (args) 
        {
            club.CreateBonus(args);
        }
        // ...
    }
}

public class Bonus
{
    public Bonus(String[] args) {}
    public string name { get; set; }
    public int discount { get; set; }
}

public class Service
{
    public Service(String[] args) {}
    public string name { get; set; }
    public int price { get; set; }
}

public class Visit
{
    public List<ActOfService> orderHistory { get; set; }
    public string date { get; set; }
    public Member member { get; set; }
    public Visit(Member member) {}
    public void CreateAOS(Service service)
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
        this.price = service.price;
        if (visit.member.bonus.discount != 0) 
        {
            price = service.price * (1 - visit.member.bonus.discount);
        }
        AssignStaff();
    }

    private void AssignStaff()
    {
        foreach (Staff staff in visit.member.club.staffList)
        {
            if (staff.isFree)
            {
                this.staff = staff;
                staff.ProvideService(this);
            }
        }
    }
}
