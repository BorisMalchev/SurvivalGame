using UnityEngine;

public class Blueprint
{
    public string ItemName;

    public string req1;
    public string req2;

    public int Req1amount;
    public int Req2amount;

    public int numOfRequirements;

    public int numberOfItensToProduce;

    public Blueprint(string name,int producedItems, int reqNUM, string R1, int R1num, string R2, int R2num)
        {

        numberOfItensToProduce = producedItems;
        ItemName = name;
        numOfRequirements = reqNUM;
        req1 = R1;
        req2 = R2;
        Req1amount = R1num;
        Req2amount = R2num;
    }
}
