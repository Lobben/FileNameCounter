# Assumptions made

## Casing in path and filename
I will assume the solution should be case insensitive. This resolves some issues where the path and file name
provided by the file system have different casing (Windows) and make multiplatform easier.
As an example filenames are case sensitive in Linux, not in Windows.

## Overlapping occurrences
I choose to take the requirement description very literally in this case and choose to count overlapping occurrences.
That means that "lol" would occur two times in "lolol".

## Encoding
Encoding is a real rabbit hole to get right if I properly want to support everything that could be interpreted as text.
To make the scope manageable for the timeframe of this task I choose to interpret every file as UTF-8. With more time
I would try to check the BOM (Byte Order Mark) before streaming in the lines, and if missing fall back to UTF-8.
If we really want to over-engineer this thing we could even try against different encodings when there is no BOM and compare the result.

## File size
I choose to read in and process one row at a time to reduce memory consumption. If the program would be used for very large files that
could potentially be without linebreaks I would read in content in batches. The tricky part would then be occurrences of the filename
that would potentially be split into two different batches...