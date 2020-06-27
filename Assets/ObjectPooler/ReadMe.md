
First add ObjectPooler somewhere in your scene
Then create all necessary pools for each object that you need to pool
To create new pool just right click somewhere in project window and go to ObjectPooler -> Pool
Set up all created scriptable object asset and add it to Pools in ObjectPooler in your scene
To spawn object use ObjectPooler.Instance.SpawnObject() with its different overrides
To spawn random objects from objects group create all necessary groups
Add all necessary pools to PoolsInGroup and then add group asset to PoolGroups in ObjectPooler in your scene
To spawn random object use ObjectPooler.Instance.SpawnRandomObject() with its different overrides