using System.Collections.Generic;
using System;

[Serializable]
public class CellContent
{
    public string sprite;
    public int qty;
    public string description;
    public int price;
    public string playerImage;
    public string playerName;
    public int playerRating;
}

[Serializable]
public class CloseButton
{
    public string closeButton;
}
[Serializable]
public class ArrowButton
{
    public string arrowButton;
}

[Serializable]
public class CellsList
{
    public CloseButton closeButton;
    public ArrowButton arrowButton;
    public List<CellContent> items;
}
