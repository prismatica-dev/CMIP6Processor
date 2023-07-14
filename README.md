# CMIP6Processor
This tool exists to process global mean near-surface air temperature data from multiple modelled data sets retrieved from https://cmip6.science.unimelb.edu.au/search. 
The data this site provides regularly is heavily bloated in a difficult to manage data format, which this tool heavily simplifies to just relevant data formatted by line separated values. Additionally it outputs this data in celcius as well, as the site only provides kelvin for near-surface air temperature.

Search Parameters used for testing dataset:
- Variable ID: Near-Surface Air Temperature (tas, K)
- CMIP Era: CMIP6
- Normalisation: Raw
- Experiment: ssp585
- Timeseries Type: Annual-mean
- Region: World
