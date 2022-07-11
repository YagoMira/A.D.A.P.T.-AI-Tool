using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    //Singleton pattern for get only one ocurrence from the world state
    //And prevent any possible failures/bugs from multiple world state initializations.

    private static readonly World instance = new World();
    private static States world;
    
    static World()
    {
        world = new States();
    }

    private World()
    {

    }

    public static World Instance
    {
        get { return instance; }
    }

    public States GetWorldState()
    {
        return world;
    }

}
