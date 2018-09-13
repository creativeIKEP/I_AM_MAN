using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public Text rankingText;
    
    int breakNum = 0;
    int reminginBattery = 0;
    int reminingTime = 0;
    int thisRank = -1;

    string table = "ranking";
    

    public void SetPram(int num, float b, float t, int l)
    {
        breakNum = num;
        reminginBattery = (int)b;
        reminingTime = (int)t;
        if (l == 1) { table = "ranking"; rankingText.text = "<color=#0000ff>LEVEL1 "; }
        else if (l == 2) { table = "ranking2"; rankingText.text = "<color=#00ff00>LEVEL2 "; }
        else if (l == 3) { table = "ranking3"; rankingText.text = "<color=#ff0000>LEVEL3 "; }
        
        Debug.Log("this Record:" + "num=" + num + ", battery=" + b + ", time=" + t);
        rankingText.text += "RANKING TABLE</color>\n\n<color=#000000>Rank\tScore\tRemingBattery\tRemingTime</color>\n";
        Rank();
    }

    void Rank()
    {
        int currentRank = 0;
        SqliteDatabase sqlDB = new SqliteDatabase("rank.db");
        
        if (reminingTime == 0)
        {
            
            string query = "select * from "+table+" where time=0 order by rank asc";
            DataTable dataTable = sqlDB.ExecuteQuery(query);

            foreach (DataRow dr in dataTable.Rows)
            {
                int rank = (int)dr["rank"];
                int score = (int)dr["score"];
                int battery = (int)dr["battery"];
                int time = (int)dr["time"];
                currentRank = rank;
                Debug.Log(rank + ", " + score + ", " + battery + ", " + time);
                if (breakNum > score)
                {
                    thisRank = rank;
                    ResetRank(rank);
                    query = "insert into "+table+"(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
                    sqlDB.ExecuteQuery(query);
                    DisplayRanking();
                    return;
                }
                else if (breakNum == score)
                {
                    query = "select * from " + table + " where time=0 and score=" + breakNum + " order by rank asc";
                    dataTable = sqlDB.ExecuteQuery(query);
                    foreach (DataRow dr2 in dataTable.Rows)
                    {
                        int rank2 = (int)dr2["rank"];
                        int score2 = (int)dr2["score"];
                        int battery2 = (int)dr2["battery"];
                        int time2 = (int)dr2["time"];
                        currentRank = rank2;
                        if (reminginBattery >= battery2)
                        {
                            thisRank = rank2;
                            ResetRank(rank2);
                            query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
                            sqlDB.ExecuteQuery(query);
                            DisplayRanking();
                            return;
                        }
                    }
                    thisRank = currentRank + 1;
                    ResetRank(currentRank + 1);
                    query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
                    sqlDB.ExecuteQuery(query);
                    DisplayRanking();
                    return;
                }
            }
            thisRank = currentRank + 1;
            ResetRank(currentRank + 1);
            query = "insert int " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
            sqlDB.ExecuteQuery(query);
            DisplayRanking();
            return;
        }

        else if (reminginBattery == 0)
        {

            string query2 = "select * from " + table + " order by rank asc";
            DataTable dataTable2 = sqlDB.ExecuteQuery(query2);

            string query = "select * from " + table + " where battery=0 and time>0 order by rank asc";
            DataTable dataTable = sqlDB.ExecuteQuery(query);

            currentRank = dataTable2.Rows.ToArray().Length - dataTable.Rows.ToArray().Length;
            foreach (DataRow dr in dataTable.Rows)
            {
                int rank = (int)dr["rank"];
                int score = (int)dr["score"];
                int battery = (int)dr["battery"];
                int time = (int)dr["time"];
                currentRank = rank;
                if (breakNum > score)
                {
                    thisRank = rank;
                    ResetRank(rank);
                    query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
                    sqlDB.ExecuteQuery(query);
                    DisplayRanking();
                    return;
                }
                else if (breakNum == score)
                {
                    query = "select * from " + table + " where battery=0 and score=" + breakNum + " order by rank asc";
                    dataTable = sqlDB.ExecuteQuery(query);
                    foreach (DataRow dr3 in dataTable.Rows)
                    {
                        int rank2 = (int)dr3["rank"];
                        int score2 = (int)dr3["score"];
                        int battery2 = (int)dr3["battery"];
                        int time2 = (int)dr3["time"];
                        currentRank = rank2;
                        if (reminingTime <= time2)
                        {
                            thisRank = rank2;
                            ResetRank(rank2);
                            query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
                            sqlDB.ExecuteQuery(query);
                            DisplayRanking();
                            return;
                        }
                    }
                    thisRank = currentRank + 1;
                    ResetRank(currentRank + 1);
                    query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
                    sqlDB.ExecuteQuery(query);
                    DisplayRanking();
                    return;
                }
            }
            thisRank = currentRank + 1;
            ResetRank(currentRank + 1);
            query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
            sqlDB.ExecuteQuery(query);
            DisplayRanking();
            return;
        }
    }

    void ResetRank(int r)
    {
        int rr = r;
        SqliteDatabase sqlDB = new SqliteDatabase("rank.db");
        string query = "update " + table + " set rank=rank+1 where rank>=" + r;
        sqlDB.ExecuteQuery(query);
    }

    public void DisplayRanking()
    {
        SqliteDatabase sqlDB = new SqliteDatabase("rank.db");
        string query = "select * from " + table + " order by rank asc";
        DataTable dataTable = sqlDB.ExecuteQuery(query);
        foreach (DataRow dr in dataTable.Rows)
        {
            int rank = (int)dr["rank"];
            int score = (int)dr["score"];
            int battery = (int)dr["battery"];
            int time = (int)dr["time"];
            
            Debug.Log("rank=" + rank + ", score=" + score + ", battery=" + battery + ", time=" + time);
            if (rank == thisRank && score == breakNum && battery == reminginBattery && time == reminingTime)
            {
                Debug.Log("^^^^this is your record^^^^");
                rankingText.text +="<color=#00ff00>" + string.Format("{0,-3}", rank) + "\t\t\t\t" + string.Format("{0,-3}", score) + "\t\t\t\t" + string.Format("{0,-3}", battery) + "\t\t\t\t" + string.Format("{0,-3}", time) + "</color>\n";
            }
            else
            {
                string.Format("{0,3}", rank);
                rankingText.text += string.Format("{0,-3}", rank) + "\t\t\t\t" + string.Format("{0,-3}", score) + "\t\t\t\t" + string.Format("{0,-3}", battery) + "\t\t\t\t" + string.Format("{0,-3}", time) + "\n";
            }
        }
        
    }
}
