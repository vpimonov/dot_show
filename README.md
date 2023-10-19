# dot_show
Simple graphical logger for points come from communication port

## input data
Each data frame come as string terminated '\n'
Frame contains numbers delimited by ','
Couple of numbers represent 2D point coordinates X,Y
Same as X1, Y1, X2, Y2 ... \n

## output data
After capturing a new frame, the previous one is cleared, and the new frame is displayed in the main window as a set of points.
