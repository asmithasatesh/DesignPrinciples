using System;
using System.Collections.Generic;
using System.Text;

namespace Design_Principles
{
    //Interface Segregation Principle
    interface TaxCalculator
    {
        public decimal CalculateIncomeTax(decimal basicPay);
    }
    interface specialAllowances
    {
        public decimal TravelandFoodAllowance(string employeeType);
    }
    public class SingleResponsibilityPrinciple
    {
        public static List<SingleResponsibilityPrinciple> list = new List<SingleResponsibilityPrinciple>();
        public  string employeeName { get; set; }
        public int age { get; set; }
        public  decimal basicPay { get; set; }
        public  decimal Deduction { get; set; }
        public string employeeType { get; set; }
        public SingleResponsibilityPrinciple(string employeeName, int age, decimal basicPay, decimal Deduction,string employeeType)
        {
            this.employeeName = employeeName;
            this.age = age;
            this.basicPay = basicPay;
            this.Deduction = Deduction;
            this.employeeType = employeeType;
        }
        //Method to Get Employee Details
        public static List<SingleResponsibilityPrinciple> GetInput()
        {
            Console.WriteLine("Enter the number of Employees");
            int n= Convert.ToInt32(Console.ReadLine());
            while(n--!=0)
            {
                //Employee Salary Computation
                Console.WriteLine("\nEnter the EmployeeName:");
                string employeeName = Console.ReadLine();
                Console.WriteLine("Enter the Age:");
                int age = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the Basic Pay:");
                decimal basicPay = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Enter the Deduction:");
                decimal Deduction = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Enter the Employee Type:");
                string employeeType = Console.ReadLine();
                SingleResponsibilityPrinciple singleResponsibilityPrinciple = new SingleResponsibilityPrinciple(employeeName, age, basicPay, Deduction,employeeType);
                list.Add(singleResponsibilityPrinciple);
            }
            return list;
        }
    }

    public class DisplayEmployeeDetail
    {
        //Display Employee Details
        public void Display()
        {
            List<SingleResponsibilityPrinciple> employeeDetails = SingleResponsibilityPrinciple.GetInput();
            decimal netPay = 0;
            Console.WriteLine("\n--------------------- Display Employee Details ---------------------");
            foreach (var i in employeeDetails)
            {
                Console.WriteLine("\nEmployeeName: "+i.employeeName);
                Console.WriteLine("Enter the Age: "+i.age);
                Console.WriteLine("Enter the Basic Pay: "+i.basicPay);
                Console.WriteLine("Enter the Deduction: "+i.Deduction);
                Console.WriteLine("Enter the Employee Type: " + i.employeeType);
                //Liskov Substitution Principle
                if (i.employeeType =="FullTime")
                {
                    CalculateFunction fullTimeEmployee = new FullTimeEmployee();
                    netPay = fullTimeEmployee.CalculateNetPay(i);
                }
                else
                {
                    CalculateFunction partTimeEmployee = new PartTimeEmployee();
                    netPay = partTimeEmployee.CalculateNetPay(i);
                }

                Console.WriteLine("Enter the NetPay: " + netPay);
            }
            //Create interface object for FullTime
            DisplayEmployeeNames displayEmployeeNames = new PrintFullTime();
            //Create object for Interface Manager
            EmployeeTypeManager employeeTypeManager = new EmployeeTypeManager(displayEmployeeNames);
            //Method
            employeeTypeManager.Display(employeeDetails);
            //Create interface object for Part Time
            DisplayEmployeeNames displayEmployeeName = new PrintPartTime();
            EmployeeTypeManager employeeTypeManagers = new EmployeeTypeManager(displayEmployeeName);
            employeeTypeManagers.Display(employeeDetails);

        }
    }
    public interface DisplayEmployeeNames
    {
        public void PrintName(List<SingleResponsibilityPrinciple> employeeDetails);
    }

    //Dependency Inversion
    public class EmployeeTypeManager
    {
        //Create object for Interface
        DisplayEmployeeNames displayEmployeeNames;
        public EmployeeTypeManager(DisplayEmployeeNames displayEmployeeNames)
        {
            this.displayEmployeeNames = displayEmployeeNames;
        }
        //Call Interface Method
        public void Display(List<SingleResponsibilityPrinciple> employeeDetails)
        {
            displayEmployeeNames.PrintName(employeeDetails);
        }
    }
    //Implement Interface
    public class PrintFullTime : DisplayEmployeeNames
    {
        public void PrintName(List<SingleResponsibilityPrinciple> employeeDetails)
        {
            Console.WriteLine("-- FullTime--");
            foreach (var employee in employeeDetails)
            {
                if (employee.employeeType == "FullTime")
                {
                    Console.WriteLine("Employee Name : " + employee.employeeName);
                }
            }
        }
    }
    
    public class PrintPartTime : DisplayEmployeeNames
    {
        public void PrintName(List<SingleResponsibilityPrinciple> employeeDetails)
        {
            Console.WriteLine("-- PartTime--");
            foreach (var employee in employeeDetails)
            {
                if (employee.employeeType != "FullTime")
                {
                    Console.WriteLine("Employee Name : " + employee.employeeName);
                }
            }
        }
    }


    //Interface Segregation Principle
    public class TaxCalculate: TaxCalculator
    {
        public  decimal CalculateIncomeTax(decimal basicPay)
        {
            if(basicPay< 5000000)
            {
                return 0;
            }
            decimal incomeTax = (basicPay * 5 )/ 100 ;
            return incomeTax;
        }
    }
    //Open Closed Principle
    public abstract class CalculateFunction
    {
        //Calculate NetPay
        public abstract decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails);
    }
    //Calculate Netpay for fulltime Employee
    public class FullTimeEmployee : CalculateFunction,specialAllowances
    {
        TaxCalculate TaxCalculate = new TaxCalculate();
        public decimal TravelandFoodAllowance(string employeeType)
        {
            decimal allowances = 0;
            if(employeeType== "FullTime")
            {
                allowances = 1000;
            }
            return allowances;
        }
        public override decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails)
        {
            decimal netPay = employeeDetails.basicPay - employeeDetails.Deduction + TravelandFoodAllowance(employeeDetails.employeeType) - TaxCalculate.CalculateIncomeTax(employeeDetails.basicPay);
            return netPay;
        }

    }
    //Calculate Netpay for Parttime Employee

    public class PartTimeEmployee : FullTimeEmployee
    {
        TaxCalculate TaxCalculate = new TaxCalculate();
        public override decimal CalculateNetPay(SingleResponsibilityPrinciple employeeDetails)
        {
            decimal netPay = employeeDetails.basicPay - (employeeDetails.Deduction / 2) - TaxCalculate.CalculateIncomeTax(employeeDetails.basicPay);
            return netPay;
        }
    }

}
