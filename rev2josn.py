'''
Simple socket server using threads
it prints whatever clients sent to it on its terminal.
'''





import socket
import sys
from thread import *
import re

#connet to influxDB and create create a database called Jsom_Text




HOST = '172.31.26.71'   # Symbolic name meaning all available interfaces
PORT = 8084 # Arbitrary non-privileged port





s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
print 'Socket created'

#Bind socket to local host and port
try:
	s.bind((HOST, PORT))
except socket.error as msg:
	print 'Bind failed. Error Code : ' + str(msg[0]) + ' Message ' + msg[1]
	sys.exit()

	print 'Socket bind complete'

#Start listening on socket
s.listen(10)
print 'Socket now listening'





class Receiver:

	def write2file(self,fp,timeList,eyeList,nodList,smileList):
		
		if(len(timeList)!=0):
			
			for timeindex in range(len(timeList)):
					fp.write( timeList[timeindex]+","+str(eyeList[timeindex])+","+str(nodList[timeindex])+","+str(smileList[timeindex])+"\n");
			



#Function for handling connections. This will be used to create threads
	def clientthread(self,conn):
		
		#open a file
		fp = open("/var/www/html/csv/nonverbal_data.csv", "w+")

		#write item name to the file
		fp.write('timeStamp,eyeContactIntensity,nodIntensity,smileIntensity\n') 
		timeList=[]
		eyeList=[]
		nodList=[]
		smileList=[]
		bufnum=0
		
		rev_buffer=''



		#infinite loop so that function do not terminate and thread do not end.
		while True:
				recv_data = conn.recv(65536)				
				bufnum+=1
				
				#break if no recv data avalible
				if not recv_data: 
					break
				rev_buffer+=recv_data
			 
				


		#come out of loop
		timeSet=" "
		timeSet += " ".join(re.findall("\"timeStamp\": (.*?)Z\"", rev_buffer))
		
		
		temp=timeSet.split(' \"')
		for i in range(len(temp)):
			if temp[i]!='' and temp[i]!=' ':
				
					timeList.append(temp[i])							
		del temp[:]
		



		eyeSet = " ".join(re.findall("\"eyeContactIntensity\": (.*?),", rev_buffer))
		
		temp=eyeSet.split(' ')		
		for i in range(len(temp)):
			if temp[i]!= '-1':
					eyeList.append(temp[i])
		del temp[:]
		

		nodSet = " ".join(re.findall("\"nodIntensity\": (.*?),", rev_buffer))
		
		temp=nodSet.split(' ')		
		for i in range(len(temp)):
			if temp[i]!= '-1':
					nodList.append(temp[i])
		del temp[:]
		

		smileSet = " ".join(re.findall("\"smileIntensity\": (.*?),", rev_buffer))
		temp=smileSet.split(' ')				
		for i in range(len(temp)):
			if temp[i]!= '-1':
					smileList.append(temp[i])
		del temp[:]
		
		#write to a file
		self.write2file(fp,timeList,eyeList,nodList,smileList)
		
		#close file
		fp.close()
		
		#close conn
		conn.close()
		print ('Used', bufnum, '65536-byte buffers')
		
	

# set up a Receiver objects array of size 10
guest = []
guestNum=0
for i in range(10):
	receiver = Receiver()
	guest.append(receiver)


#now keep talking with the client
while 1:
	#wait to accept a connection - blocking call
	conn, addr = s.accept()
	print 'Connected with ' + addr[0] + ':' + str(addr[1])
	
	# setup Receiver object

	#start new thread takes 1st argument as a function name to be run, second is the tuple of arguments to the function.
	if guestNum < 10:
		guestNum+=1
		start_new_thread(guest[guestNum].clientthread ,(conn,))

	else:
		print('Sorry! Connection maximum of the server is reached!')
		s.close()
