# DuplicateLineReader

Checks if file has duplicate lines.

Example text file:
```
0. NOT
1.
2. DUPLICATE
3. 
4.  LINE 1
5. 
6. LET
7. LET
8. 
9. LINE 1
10. 
11. DUPLICATE
12. DUPLICATE
13. 
14. 
15. HAS
```
Result will be:
```
DUPLICATE: 2,11,12
LINE 1: 4,9
LET: 6,7
```
Version v1.0.0:
https://github.com/dan-dev/DuplicateLineReader/releases
