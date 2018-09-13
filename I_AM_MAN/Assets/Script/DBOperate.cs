using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBOperate : MonoBehaviour {
    public int score;
    public int battery;
    public int time;
    public bool show;
    public bool insert;
    public bool deleteAll;
    public int tableNo;
    string table = "ranking";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (show)
        {
            show = false;
            insert = false;
            deleteAll = false;
            if (tableNo == 1) { table = "ranking"; }
            else if (tableNo == 2) { table = "ranking2"; }
            else if (tableNo == 3) { table = "ranking3"; }
            SqliteDatabase sqlDB = new SqliteDatabase("rank.db");
            string query = "select * from "+table+" order by rank asc";
            DataTable dataTable = sqlDB.ExecuteQuery(query);
            foreach (DataRow dr in dataTable.Rows)
            {
                int rank = (int)dr["rank"];
                int score = (int)dr["score"];
                int battery = (int)dr["battery"];
                int time = (int)dr["time"];
                Debug.Log("rank=" + rank + ", score=" + score + ", battery=" + battery + ", time=" + time);
            }
        }

        if (insert)
        {
            show = false;
            insert = false;
            
            deleteAll = false;
            if (tableNo == 1) { table = "ranking"; }
            else if (tableNo == 2) { table = "ranking2"; }
            else if (tableNo == 3) { table = "ranking3"; }
            DataIn(score, battery, time);
        }

        
        if (deleteAll)
        {
            show = false;
            insert = false;
            
            deleteAll = false;
            if (tableNo == 1) { table = "ranking"; }
            else if (tableNo == 2) { table = "ranking2"; }
            else if (tableNo == 3) { table = "ranking3"; }
            SqliteDatabase sqlDB = new SqliteDatabase("rank.db");
            string query = "delete from "+table;
            DataTable dataTable = sqlDB.ExecuteQuery(query);
        }
	}

    void DataIn(int s, int b, int t)
    {
        int breakNum = s;
        int reminginBattery = b;
        int reminingTime = t;
        int thisRank = -1;
        int currentRank = 0;
        SqliteDatabase sqlDB = new SqliteDatabase("rank.db");
        
        if (reminingTime == 0)
        {
            
            string query = "select * from " + table + " where time=0 order by rank asc";
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
            thisRank = currentRank+1;
            ResetRank(currentRank+1);
            query = "insert into " + table + "(rank,score,battery,time) values(" + thisRank + "," + breakNum + "," + reminginBattery + "," + reminingTime + ")";
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

            currentRank = dataTable2.Rows.ToArray().Length- dataTable.Rows.ToArray().Length;
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

    void DisplayRanking()
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
            
        }
    }
}
