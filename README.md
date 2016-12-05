        # timechart

###Use C3 js to draw a timeseries linechart for the data collected by kinect



##Description
* **Form1.cs send the data in json through TCP protocal to rev2json.py which is located on an AWS cloud virtual machine 54.191.185.244.**
* **rev2json.py categorizes the data it received and write it to nonverbal_data.csv in /var/www/html/csv dir on AWS virtual machine  .** 
* **linechart.html ,which is located in /var/www/html/csv dir on AWS virtual machine reads in the nonverbal_data.csv gernerated by rev2json.py to draw nonverbal feature score to linechart.** 


* **Execution Format**




        ```shell

        $ sudo python rev2json.py

        ```
        ;then,execute Form1.cs by Visual Studio to send data through TCP to rev2json.py,the nonverbal_data.csv will be set up under /var/www/html/csv dir on AWS virtual machine 54.191.185.244.
