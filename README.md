# Zarjaz Prints

Refactor of an old test project to .Net Core 3.1

Zarjaz Prints is an online store selling prints of classic 2000AD covers - bit more interesting than Northwind :)


## Old version

The old version dates from around 2015 and was built on top of elasticsearch 1.7. If you can exhume a copy that old it might even work.

The old version projects are:

* Data
 * The data entities layer. Got to have one of those, right? Even if there's only one entity in the project...
* Indexer
  * Command line tool for bulk loading data into elasticsearch
* ZarjazPrints
  * NancyFX web front end

## Shiny new 2020 version

The new version is rebuilt in .Net Core 3.1, and heavily refactored. It's built on top of an in-memory data store to make it easier to run out of the box without needing to set up a database first.

## Example data

The ```SourceData``` folder contains example json data and images for the first 50 progs, plus a couple of others that I used on the homepage.

Just enough to demo the site without getting too copyright-infringey (I hope)