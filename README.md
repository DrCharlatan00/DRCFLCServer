# **DRCFLCServer stores .flac audio files.**
> [!CAUTION] 
> All .flac files must be placed in the AudioFiles folder located in the same directory as the server application.
# REST Endpoints:
**/audio** — Searches for a requested .flac file and, if found, sends it.
**/list** — Returns a JSON response containing a list of all discovered .flac files.
**/alive** — Checks whether the server is running (health check endpoint).
**/version** — Returns the current version of the server.
