# Car Wash Solution. 

##### Task:
Develop program/suite modeling work of a car-wash station (see attached image).

##### Input: 
 - program should continuously generate visitors itself with random interval in time (e.g. every 1-10sec)
 - configuration parameters: A,B,X,Z (will be explained later)

##### Output:
 - collect statistics: 
   a) number of generated visitors
   b) number of rejected visitors
   c) number of processed visitors
   d) avg. processing time
   e) avg. waiting time

##### Explanation: 
Vistiors are coming at random time. Every new visitor should stay in queue before being served in one of washing bays (orange).
There are 3 washing bays each having its own waiting queue (blue). 
New visitor is choosing queue basing on its current size - always going to the shortest queue.
Each waiting queue (blue) has limitted capacity - constant/configurable value A (e.g. 3).
In case we have a new visitor when all queues are full - visitor is leaving ("rejected").
Visitors are moving from waiting queue (blue) to washing bay once washing bay becomes free (finished washing previous car).
Processing time of each washing bay is a random value with configurable Mean (X) and deviation - 2 seconds.
Once car is washed it vacates washing bay and proceeds to SINGLE waiting queue (green) before 2 drying bays (purple).
Possible collision should be avoid at this point (when 2 cars are leaving washing bays at same time).
Processing time of each drying bay is a random value with configurable Mean (Z) and deviation - 2 seconds.

## How to run the solution

Open the solution in visual studio. Set CarWash.Api.Washing , CarWash.StatsViewer , VisitorGenerators as startup projects using the set multiple startup project option in the solution properties. Execute the solution. 

### Prerequisites
.Net Core 

## Authors

* **Ali Abbas Manager** - [Profile](https://github.com/aliabbasmanager)