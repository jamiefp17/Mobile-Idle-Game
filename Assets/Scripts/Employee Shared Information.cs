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

    public static class SCustomerPositions
    {
        public static Vector2[] positions = new Vector2[16] 
        { new Vector2(0.75f, -3.25f), 
            new Vector2(0.75f, -2.25f), 
            new Vector2(0.75f, -1.25f), 
            new Vector2(0.75f, -0.25f), 
            new Vector2(4.75f, -2.75f), 
            new Vector2(5.75f, -2.75f), 
            new Vector2(4.75f, -4.25f), 
            new Vector2(5.75f, -4.25f), 
            new Vector2(7.25f, -2.75f), 
            new Vector2(8.25f, -2.75f), 
            new Vector2(7.25f, -4.25f), 
            new Vector2(8.25f, -4.25f), 
            new Vector2(7.25f, 0.25f), 
            new Vector2(8.25f, 0.25f), 
            new Vector2(7.25f, -1.25f), 
            new Vector2(8.25f, -1.25f), }; //The positions where customers can wait to be served.

        public static bool[] available = new bool[16]
            { true, true, true, true,
            true, true, true, true,
            true, true, true, true,
            true, true, true, true, };
    }
}
