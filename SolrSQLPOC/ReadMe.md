# Solr SQL POC
I figured I would just throw up my files in the GIT repository in case anybody wanted to have a look at them or try to use them.
##PreciseSolr
These contain the two files I am using to configure my Vagrant box. One thing to note that the Java SQL "provider" is included in there as well as the start to some configuration files. Those can be used in any Solr install.

## SQL
Contains a SQL script which will initiate a db with some sample logs. This is the query which I was going to use for the Solr Collection:

```sql
    select LogID, ServerName, DateRequestEnd, RequestURIBase from vwLogs
```
