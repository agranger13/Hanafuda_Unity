[System.Serializable]
public class Player
{
    public int point;
    public int ID;
    public bool myTurn;
    public string playerName;

    public Player(int point, int ID)
    {
        this.point = point;
        this.ID = ID;
    }
}
