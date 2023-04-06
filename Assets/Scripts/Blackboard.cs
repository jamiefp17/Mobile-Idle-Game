using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBlackboard : MonoBehaviour
{
    //Structs that hold the data for customers and orders placed.
    public struct SCustomerData { public int customerUID; public Vector3 position; public bool beenServed; };
    public struct SOrderData { public int customerUID; public int itemID; };

    //Dictionaries to hold the information for the customers and orders.
    private Dictionary<int, SCustomerData> dCustomers;
    private Dictionary<int, SOrderData> dOrders;

    private Dictionary<int, Vector3> requestHandlers; //Holds the number of the request Handlers, and a boolean as their data.
    private Dictionary<int, BTNodes.CRequestHandler> observersList; //Holds a number that matches one in the requestHandlers dictionary, and a reference to the RH.

    //Add an element to the dictionary.
    void AddCustomer(int customerUID, Vector3 position, bool beenServed)
    {
        SCustomerData customerData = new SCustomerData
        {
            customerUID = customerUID,
            position = position,
            beenServed = beenServed
        };
        dCustomers.Add(customerUID, customerData);
    }

    void AddOrder(int customerUID, int itemID)
    {
        SOrderData orderData = new SOrderData
        {
            customerUID = customerUID,
            itemID = itemID
        };
        dOrders.Add(customerUID, orderData);
    }

    //Removes an element from the dictionary
    void RemoveCustomer(int customerUID)
    {
        dCustomers.Remove(customerUID);
    }

    void RemoveOrderr(int customerUID)
    {
        dOrders.Remove(customerUID);
    }

    public KeyValuePair<int, SCustomerData> GetCustomer(int customerUID)
    {
        if (dCustomers.TryGetValue(customerUID, out SCustomerData customerData)) // Key found
        {
            return new KeyValuePair<int, SCustomerData>(customerUID, customerData);
        }
        else // Key not found
        {
            return new KeyValuePair<int, SCustomerData>(-1, new SCustomerData { position = Vector3.zero });
        }
    }

    public KeyValuePair<BTNodes.CRequestHandler, Vector3> GetObserverData(int key)
    {
        Vector3 vector3;
        BTNodes.CRequestHandler requestHandler;

        if (requestHandlers.TryGetValue(key, out vector3) && observersList.TryGetValue(key, out requestHandler))
        {
            return new KeyValuePair<BTNodes.CRequestHandler, Vector3>(requestHandler, vector3);
        }

        // If key is not found in both dictionaries, you can handle the error case accordingly
        throw new KeyNotFoundException($"Key '{key}' not found in the dictionaries.");
    }
}
