using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Rank_Manager : MonoBehaviour
{
    /// <summary>
    /// This Manager loads ranks and Squad Rank limits
    /// </summary>
    public FileFinder finder;


    public List<string> SquadRoleFiles;
    public List<string> RankFiles;

    public List<Squad_Role_Class> squadRoles;
    public List<Rank_Class> ranks;

    public Promote_Script promoter;

    public void Begin()
    {
        DoCoreStuff();
        SquadRoleFiles = finder.Retrieve("SquadRoles.xml", ".meta");
        RankFiles = finder.Retrieve("Ranks.xml", ".meta");
        squadRoles = FindSquadFiles();
        ranks = FindRankFiles();
        promoter.SetupRoleDropdown(squadRoles);
    }

    public List<Squad_Role_Class> FindSquadFiles()
    {

        List<Squad_Role_Class> SquadRoleList = new List<Squad_Role_Class>();

        try
        {

            foreach (string s in SquadRoleFiles)
            {
                List<Squad_Role_Class> temp = Serializer.Deserialize<List<Squad_Role_Class>>(s);
                //For each Biome_Class, check for duplicates. Delete non-core biome duplicates, and overwrite core biome with duplicates.
                foreach (Squad_Role_Class tempS in temp)
                {
                    int counter = 0;
                    bool duplicate = false;
                    foreach (Squad_Role_Class checkB in SquadRoleList)
                    {
                        if (tempS.RoleName == checkB.RoleName)
                        {
                            duplicate = true;
                            if (checkB.source == "Core")
                            {
                                //Debug.Log(counter);
                                SquadRoleList[counter] = tempS;
                                Debug.Log(tempS.RoleName + " Replaced Vanilla");
                            }
                            else
                            {
                                SquadRoleList.RemoveAt(counter);
                                //Debug.Log("Duplicates Removed");
                            }
                            break;
                        }
                        counter += 1;
                    }
                    if (!duplicate)
                    {
                        SquadRoleList.Add(tempS);
                        //Debug.Log(tempB.biomeName);
                    }
                }
            }

            return SquadRoleList;
        }
        catch (UnauthorizedAccessException UAEx)
        {
            Console.WriteLine(UAEx.Message);
        }
        catch (PathTooLongException PathEx)
        {
            Console.WriteLine(PathEx.Message);
        }
        catch (FileNotFoundException FileNull)
        {
            Console.WriteLine(FileNull.Message);
        }
        return null;
    }

    public List<Rank_Class> FindRankFiles()
    {

        List<Rank_Class> RankList = new List<Rank_Class>();

        try
        {

            foreach (string s in RankFiles)
            {
                List<Rank_Class> temp = Serializer.Deserialize<List<Rank_Class>>(s);
                //For each Biome_Class, check for duplicates. Delete non-core biome duplicates, and overwrite core biome with duplicates.
                foreach (Rank_Class tempR in temp)
                {
                    int counter = 0;
                    bool duplicate = false;
                    foreach (Rank_Class checkB in RankList)
                    {
                        if (tempR.rankName == checkB.rankName)
                        {
                            duplicate = true;
                            if (checkB.source == "Core")
                            {
                                //Debug.Log(counter);
                                RankList[counter] = tempR;
                                Debug.Log(tempR.rankName + " Replaced Vanilla");
                            }
                            else
                            {
                                RankList.RemoveAt(counter);
                                //Debug.Log("Duplicates Removed");
                            }
                            break;
                        }
                        counter += 1;
                    }
                    if (!duplicate)
                    {
                        RankList.Add(tempR);
                        //Debug.Log(tempB.biomeName);
                    }
                }
            }

            return RankList;
        }
        catch (UnauthorizedAccessException UAEx)
        {
            Console.WriteLine(UAEx.Message);
        }
        catch (PathTooLongException PathEx)
        {
            Console.WriteLine(PathEx.Message);
        }
        catch (FileNotFoundException FileNull)
        {
            Console.WriteLine(FileNull.Message);
        }
        return null;
    }

    public void DoCoreStuff()
    {

        CoreRanks();
        CoreSquadRoles();
        SaveCoreRanks();
        SaveCoreSquadRoles();
    }

    public void CoreRanks()
    {
        ranks = new List<Rank_Class>();

        CreateRank("Private", "Basic", "Core");
        CreateRank("Lance Corporal", "NCO", "Core");
        CreateRank("Corporal", "NCO", "Core");
        CreateRank("Sergeant", "NCO", "Core");
        CreateRank("Gunnery Sergeant", "NCO", "Core");
        CreateRank("Staff Sergeant", "NCO", "Core");
        CreateRank("2nd Lieutenant", "Officer", "Core");
        CreateRank("Lieutenant", "Officer", "Core");
        CreateRank("Captain", "Officer", "Core");
        CreateRank("Major", "Officer", "Core");
        CreateRank("Lieutenant-Colonel", "Officer", "Core");
        CreateRank("Colonel", "Officer", "Core");
        CreateRank("Commander", "Officer", "Core");
    }

    public void CoreSquadRoles()
    {
        squadRoles = new List<Squad_Role_Class>();

        List<Rank_Definition> ranks = new List<Rank_Definition>();

        Rank_Definition rank = new Rank_Definition();
        rank.RankName = "Sergeant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Corporal";
        rank.RankLimit = 2;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Lance Corporal";
        rank.RankLimit = 3;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Private";
        rank.RankLimit = 0;
        ranks.Add(rank);

        CreateSquadRole("Infantry - Line", "Infantry", "Core", ranks); // Basic Infantry to deal with anything - no officers

        ranks = new List<Rank_Definition>();

        rank = new Rank_Definition();
        rank.RankName = "Major";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Captain";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Lieutenant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Second Lieutenant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Staff Sergeant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Sergeant";
        rank.RankLimit = 2;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Corporal";
        rank.RankLimit = 3;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Lance Corporal";
        rank.RankLimit = 3;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Private";
        rank.RankLimit = 0;
        ranks.Add(rank);

        CreateSquadRole("Infantry - Combat HQ", "Infantry", "Core", ranks); //Lower HQ command unit, limits the rank of officers but allows NCOs

        ranks = new List<Rank_Definition>();

        rank = new Rank_Definition();
        rank.RankName = "Commander";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Colonel";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Lieutenant-Colonel";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Major";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Captain";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Private";
        rank.RankLimit = 0;
        ranks.Add(rank);

        CreateSquadRole("Infantry - Command HQ", "Infantry", "Core", ranks); //Top HQ Command unit, no NCOs

        ranks = new List<Rank_Definition>();

        rank = new Rank_Definition();
        rank.RankName = "Gunnery Sergeant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Sergeant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Corporal";
        rank.RankLimit = 2;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Lance Corporal";
        rank.RankLimit = 4;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Private";
        rank.RankLimit = 0;
        ranks.Add(rank);

        CreateSquadRole("Infantry - Support", "Infantry", "Core", ranks); //Lower HQ command unit, limits the rank of officers but allows NCOs

        ranks = new List<Rank_Definition>();

        rank = new Rank_Definition();
        rank.RankName = "Gunnery Sergeant";
        rank.RankLimit = 1;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Sergeant";
        rank.RankLimit = 2;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Corporal";
        rank.RankLimit = 3;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Lance Corporal";
        rank.RankLimit = 4;
        ranks.Add(rank);

        rank = new Rank_Definition();
        rank.RankName = "Private";
        rank.RankLimit = 0;
        ranks.Add(rank);

        CreateSquadRole("Infantry - Elite", "Infantry", "Core", ranks); //Lower HQ command unit, limits the rank of officers but allows NCOs
    }

    public void SaveCoreRanks()
    {
        var file = File.Create(finder.defaultPath + "/Ranks/CoreRanks.xml");
        file.Close();
        Serializer.Serialize(ranks, finder.defaultPath + "/Ranks/CoreRanks.xml");
        Debug.Log("Rank File written");
    }

    public void SaveCoreSquadRoles()
    {
        var file = File.Create(finder.defaultPath + "/Ranks/CoreSquadRoles.xml");
        file.Close();
        Serializer.Serialize(squadRoles, finder.defaultPath + "/Ranks/CoreSquadRoles.xml");
        Debug.Log("Role File written");
    }

    private void CreateRank(string rankName, string rankType, string source)
    {
        Rank_Class rank = new Rank_Class();

        rank.rankName = rankName;
        rank.rankType = rankType;
        rank.source = source;

        ranks.Add(rank);
    }

    private void CreateSquadRole(string roleName, string roleType, string source, List<Rank_Definition> rankDefs)
    {
        Squad_Role_Class role = new Squad_Role_Class();

        role.RoleName = roleName;
        role.RoleType = roleType;
        role.source = source;
        role.RankDefs = rankDefs;

        squadRoles.Add(role);
    }

    public Squad_Role_Class FindRole(string squadRole)
    {
        foreach(Squad_Role_Class src in squadRoles)
        {
            if(src.RoleName == squadRole)
            {
                return src;
            }
        }
        return null;
    }
}
