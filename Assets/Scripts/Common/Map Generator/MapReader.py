#!/usr/bin/env python

from PIL import Image
import sys, getopt, csv, random

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
   print('=============================================================')
   board, playerSpawns, cpuSpawns = ReadPNG(inputfile)
   WriteAsset(inputfile, outputfile, board, cpuSpawns, playerSpawns)

def ReadPNG(inputfile):

	heightmap = [(255,216,0),(182,255,0),(76,255,0),(0,255,33),(0,255,144),(0,255,255)]
	playerSpawn = (0,0,0)
	cpuSpawn = (255,0,0)

	board = []
	cpuSpawns = []
	playerSpawns = []

	im = Image.open(inputfile)
	im = im.convert('RGB')
	width, height = im.size

	for x in range(width):
		for y in range(height):
			r,g,b = im.getpixel((x,y))
			if (r,g,b)==playerSpawn:
				tileheight = 3
				playerSpawns.append((x,y,tileheight))
				board.append((x,y,tileheight))
			elif (r,g,b)==cpuSpawn:
				tileheight = 3
				cpuSpawns.append((x,y,tileheight))
				board.append((x,y,tileheight))
			elif (r,g,b)==(255,255,255):
				pass # whitespace means no tile
			else:
				tileheight = heightmap.index((r,g,b))+1
				board.append((x,y,tileheight))

	return board, playerSpawns, cpuSpawns

def WriteAsset(inputfile, outputfile, board, cpuSpawns, playerSpawns):
	file = open(outputfile)
	lines = file.readlines()
	file.close()
	
	finalAsset = []

	#remove any current tiles and store everything else
	for line in lines:
		if(line[0:8] == '  - {x: '):
			pass # do nothing
		else:
			finalAsset.append(line)

	#write in new tiles
	tileIndex = finalAsset.index('  tiles:\n')+1
	
	for tile in board:
		formattedString = f'  - {{x: {tile[0]}, y: {tile[2]}, z: {tile[1]}}}\n'
		try:
			finalAsset.insert(tileIndex, formattedString)
		except IndexError:
			finalAsset.append(tileIndex, formattedString)

		tileIndex += 1

	#write in new playerSpawns
	playerSpawnsIndex = finalAsset.index('  playerSpawns:\n')+1
	
	for spawn in playerSpawns:
		formattedString = f'  - {{x: {spawn[0]}, y: {spawn[2]}, z: {spawn[1]}}}\n'
		try:
			finalAsset.insert(playerSpawnsIndex, formattedString)
		except IndexError:
			finalAsset.append(playerSpawnsIndex, formattedString)

		playerSpawnsIndex += 1

	#write in new cpuSpawns
	cpuSpawnsIndex = finalAsset.index('  cpuSpawns:\n')+1
	
	for spawn in cpuSpawns:
		formattedString = f'  - {{x: {spawn[0]}, y: {spawn[2]}, z: {spawn[1]}}}\n'
		try:
			finalAsset.insert(cpuSpawnsIndex, formattedString)
		except IndexError:
			finalAsset.append(cpuSpawnsIndex, formattedString)

		cpuSpawnsIndex += 1

	#convert to string for writing to file
	finalAssetString = ''
	for line in finalAsset:
		finalAssetString += line

	#test output
	print(finalAssetString)

	if(input('Is this ok? ') == 'y'):
		assetfilename = inputfile[:-4]+'.asset'
		file = open(assetfilename, 'w')
		file.write(finalAssetString)
		file.close()
		print('Wrote to file.')
	else:
		print('Did not write to file.')


if __name__ == "__main__":
	main(sys.argv[1:])