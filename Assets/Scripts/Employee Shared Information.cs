using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EmployeeSharedInformation
{
    public class CEmployeeSharedInformation
    {
        //Customer Information
        List<SCustomerData> customers = new List<SCustomerData>();
        //Order Information
        List<SOrderData> orders = new List<SOrderData>();
    }

    struct SCustomerData
    {
        int customerID; //Unique identifier for each customer.
        bool beenServed; //Whether an employee has spoken to the customer and found out what they would like to order yet.
        Vector2 position; //The x and y position of the customer in the scene.
    }

    struct SOrderData
    {
        int customerID; //Matches the ID of the customer who made the order, so when ready, it can be taken to the correct person.
        int orderID; //Unique identifier for each order.
        bool available; //Whether the order is available for an employee to make, or is being made by another employee.

        enum order //All of the options that a customer can choose to order.
        {
            option1,
            option2,
            option3,
            option4,
            option5,
            option6
        }
    }
}
