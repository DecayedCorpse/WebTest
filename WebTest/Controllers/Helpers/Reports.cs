﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.Common;
using System.Text.Json;
using System.Runtime.Serialization.Json;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using WebTest.Models;
using Microsoft.EntityFrameworkCore.Internal;

class Reports
{
    DBSets DBSets;

    public Reports(DBSets sets)
    {
        DBSets = sets;
    }

    public string? GetReport(ReportConfiguration report)
    {
        switch (report.Type.ToLower())
        {
            case "заявки":
                if (report.Category == "Все") report.Category = "";
                var result =
                    from application in DBSets.Applications
                    join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
                    join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
                    join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
                    join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
                    join City in DBSets.Cities on Address.CityID equals City.CityID
                    join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
                    join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
                    join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
                    join User in DBSets.Users on application.UserID equals User.UserID
                    join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID
                    where application.CreatedAt >= report.From && application.CreatedAt <= report.To
                    where ApplicationStatus.Name.ToLower().Contains(report.Category.ToLower())

                    select new ReportModel()
                    {
                        ApplicationID = application.ApplicationID.ToString(),
                        CreatedAt = application.CreatedAt.ToString(),
                        Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                        Classifier = Classifier.Name,
                        UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                        UserPhone = User.PhoneNumber,
                        Company = Company.Name,
                        Description = application.Description.Replace("<p>", "").Replace("</p>", "")
                    };

                return new ModelToJson<ReportModel>() { Models = result.Select(l => l) }.JsonToString();
            default:
                return "";
        }
    }

    public string? GetReportByCompanies(ReportConfiguration report)
    {
        var result =
            from application in DBSets.Applications
            join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
            join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
            join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
            join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
            join User in DBSets.Users on application.UserID equals User.UserID
            join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID
            where application.CreatedAt >= report.From && application.CreatedAt <= report.To
            where report.Specifications.Contains(Company.Name.ToLower())

            select new ReportModel()
            {
                ApplicationID = application.ApplicationID.ToString(),
                CreatedAt = application.CreatedAt.ToString(),
                Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                Classifier = Classifier.Name,
                UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                UserPhone = User.PhoneNumber,
                Company = Company.Name,
                Description = application.Description.Replace("<p>", "").Replace("</p>", "")
            };

        return new ModelToJson<ReportModel>() { Models = result.Select(l => l) }.JsonToString();
    }

    public string? GetReportByClassifiers(ReportConfiguration report)
    {
        var result =
            from application in DBSets.Applications
            join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
            join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
            join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
            join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
            join User in DBSets.Users on application.UserID equals User.UserID
            join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID
            where application.CreatedAt >= report.From && application.CreatedAt <= report.To
            where report.Specifications.Contains(Classifier.Name.ToLower())

            select new ReportModel()
            {
                ApplicationID = application.ApplicationID.ToString(),
                CreatedAt = application.CreatedAt.ToString(),
                Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                Classifier = Classifier.Name,
                UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                UserPhone = User.PhoneNumber,
                Company = Company.Name,
                Description = application.Description.Replace("<p>", "").Replace("</p>", "")
            };

        return new ModelToJson<ReportModel>() { Models = result.Select(l => l) }.JsonToString();
    }

    public string? GetReportByAddresses(ReportConfiguration report)
    {
        var result =
            from application in DBSets.Applications
            join ApplicationStatus in DBSets.ApplicationStatuses on application.ApplicationStatusID equals ApplicationStatus.ApplicationStatusID
            join Address in DBSets.Addresses on application.AddressID equals Address.AddressID
            join Region in DBSets.Regions on Address.RegionID equals Region.RegionID
            join District in DBSets.Districts on Address.DistrictID equals District.DistrictID
            join City in DBSets.Cities on Address.CityID equals City.CityID
            join Street in DBSets.Streets on Address.StreetID equals Street.StreetID
            join Building in DBSets.Buildings on Address.BuildingID equals Building.BuildingID
            join Classifier in DBSets.Classifiers on application.ClassifierID equals Classifier.ClassifierID
            join User in DBSets.Users on application.UserID equals User.UserID
            join Company in DBSets.Companies on application.CompanyID equals Company.CompanyID

            where application.CreatedAt >= report.From && application.CreatedAt <= report.To
            where report.Specifications.Contains(Region.Name.ToLower())
            where report.Specifications.Contains(District.Name.ToLower())
            where report.Specifications.Contains(City.Name.ToLower())
            where report.Specifications.Contains(Street.Name.ToLower())

            select new ReportModel()
            {
                ApplicationID = application.ApplicationID.ToString(),
                CreatedAt = application.CreatedAt.ToString(),
                Address = Region.Name + ", " + District.Name + ", " + City.Name + ", " + Street.Name + ", " + Building.Name,
                Classifier = Classifier.Name,
                UserFullName = User.LastName + " " + User.FirstName + " " + User.MiddleName,
                UserPhone = User.PhoneNumber,
                Company = Company.Name,
                Description = application.Description.Replace("<p>", "").Replace("</p>", "")
            };

        return new ModelToJson<ReportModel>() { Models = result.Select(l => l) }.JsonToString();
    }

    private bool ContainsAddress(ReportConfiguration report, string region, string district, string city, string street)
    {
        List<string> spec = report.Specifications.ToList();
        int regionIndex = report.Specifications.IndexOf(region);
        if (regionIndex != -1)
        {
            if (spec[regionIndex + 1] == district)
            {

            }
            else if (spec[regionIndex + 1] == "") return true;
            else return false;
        }
        return false;
    }

    public IEnumerable<string?> GetCompanies(string company) =>
        DBSets.Companies.Where(c => c.Name.ToLower().Contains(company.ToLower())).Select(c => c.Name).ToList();

    public IEnumerable<string?> GetClassifiers(string classifier) =>
        DBSets.Classifiers.Where(c => c.Name.ToLower().Contains(classifier.ToLower())).Select(c => c.Name).ToList();

    public IEnumerable<string?> GetRegions(string region) =>
       DBSets.Regions.Where(r => r.Name.ToLower().Contains(region.ToLower())).Select(c => c.Name).ToList();

    public IEnumerable<string?> GetDistricts(string region, string district)
    {
        var result =
            from District in DBSets.Districts
            join Region in DBSets.Regions on District.RegionID equals Region.RegionID
            where Region.Name.ToLower().Contains(region)
            where District.Name.ToLower().Contains(district)

            select new
            {
                District = District.Name
            };

        return result.Select(l => l.District);
    }

    public IEnumerable<string?> GetCities(string region, string district, string city)
    {
        var result =
            from City in DBSets.Cities
            join District in DBSets.Districts on City.DistrictID equals District.DistrictID
            join Region in DBSets.Regions on District.RegionID equals Region.RegionID
            where Region.Name.ToLower().Contains(region)
            where District.Name.ToLower().Contains(district)
            where City.Name.ToLower().Contains(city)

            select new
            {
                City = City.Name
            };

        return result.Select(l => l.City);
    }

    public IEnumerable<string?> GetStreets(string region, string district, string city, string street)
    {
        var result =
            from Street in DBSets.Streets
            join City in DBSets.Cities on Street.CityID equals City.CityID
            join District in DBSets.Districts on City.DistrictID equals District.DistrictID
            join Region in DBSets.Regions on District.RegionID equals Region.RegionID
            where Region.Name.ToLower().Contains(region)
            where District.Name.ToLower().Contains(district)
            where City.Name.ToLower().Contains(city)
            where City.Name.ToLower().Contains(street)

            select new
            {
                Street = Street.Name
            };

        return result.Select(l => l.Street);
    }


}
