
First add ObjectPooler somewhere in your scene
Then create all necessary pools for each object that you need to pool
To create new pool right click somewhere in project window and go to ObjectPooler -> Pool
Set up all created scriptable object assets and add it to Pools in ObjectPooler
You can use Prefab name as pool tag or you can change it, 
and you can use auto return concrete object types to its pool by setting auto return delay

Go to ObjectPooler -> Show pools -> add all created pools by drag&drop into "Pools" label

To spawn objects use ObjectPooler.Instance.SpawnObject() with its different overrides
To return objects to pool use ObjectPooler.Instance.ReturnObject() or ObjectPooler.Instance.DelayedReturnObject()

To spawn random objects from objects group create all necessary groups
To create new pool group right click somewhere in project window and go to ObjectPooler -> PoolGroup
Add all necessary pools to PoolsInGroup and then add group assets to PoolGroups in ObjectPooler

To spawn random objects use ObjectPooler.Instance.SpawnRandomObject() with its different overrides