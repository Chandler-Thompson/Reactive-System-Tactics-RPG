#!/usr/bin/python

import sys, getopt, csv

def main(argv):
   inputfile = ''
   outputfile = ''
   try:
      opts, args = getopt.getopt(argv,"hi:o:",["ifile=","ofile="])
   except getopt.GetoptError:
      print('test.py -i <inputfile> -o <outputfile>')
      sys.exit(2)
   for opt, arg in opts:
      if opt == '-h':
         print('test.py -i <inputfile> -o <outputfile>')
         sys.exit()
      elif opt in ("-i", "--ifile"):
         inputfile = arg
      elif opt in ("-o", "--ofile"):
         outputfile = arg
   print('Input file is ', inputfile)
   print('Output file is ', outputfile)
   board = ReadCSV(inputfile)
   WriteAsset(outputfile, board)

def ReadCSV(inputfile):
	csvfile = open(inputfile, newline='\n')
	reader = csv.reader(csvfile, delimiter=',')
	board = []
	width = 0
	depth = 0
	for row in reader:
		depth += 1
		for item in row:
			width += 1
			board.append( (width, depth, len(item)) )

	csvfile.close()
	return board

def WriteAsset(outputfile, board):
	file = open(outputfile)
	lines = file.readlines()
	
	finalAsset = []

	#remove any current tiles
	for line in lines:
		if(line[0:8] == '  - {x: '):
			pass # do nothing
		else:
			finalAsset.append(line)

	print(finalAsset)

	tileIndex = finalAsset.index('  tiles:\n')+1

	#write in new tiles
	for tile in board:
		finalAsset.insert(tileIndex, f'  - {{x: {tile[0]}, y: {tile[1]}, z: {tile[2]}}}\n')
		tileIndex += 1

	#test output
	for line in finalAsset:
		print(line)

	file.close()

if __name__ == "__main__":
   main(sys.argv[1:])