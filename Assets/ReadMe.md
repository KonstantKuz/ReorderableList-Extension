
First add ObjectPooler somewhere in your scene
Then create all necessary pools for each object that you need to pool
To create new pool right click somewhere in project window and go to ObjectPooler -> Pool
Set up all created scriptable object assets and add it to Pools in ObjectPooler
Go to ObjectPooler -> Show pools -> add all created pools by drag&drop into "Pools" label

To spawn objects use ObjectPooler.Instance.SpawnObject() with its different overrides

To spawn random objects from objects group create all necessary groups
To create new pool group right click somewhere in project window and go to ObjectPooler -> PoolGroup
Add all necessary pools to PoolsInGroup and then add group assets to PoolGroups in ObjectPooler

To spawn random objects use ObjectPooler.Instance.SpawnRandomObject() with its different overrides