﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

/*
Author : Oshan Mendis
ID     : 19222071
Last modified date : 12/04/2018
Reference : TrueMarbleData(Program.cs)
*/

namespace TrueMarbleBiz
{
    //This class contains main method which initialises the biz tier server object
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NetTcpBinding ntb = new NetTcpBinding();

                //To relax the maximum size of a message in .net(Since images are being passed arround)
                ntb.MaxReceivedMessageSize = System.Int32.MaxValue;
                ntb.ReaderQuotas.MaxArrayLength = System.Int32.MaxValue;

                ServiceHost sh = new ServiceHost(typeof(TMBizControllerImpl));
                sh.AddServiceEndpoint(typeof(ITMBizController), ntb, "net.tcp://localhost:50002/TMBiz");

                sh.Open();
                System.Console.WriteLine("Press enter to close the biz tier");
                System.Console.ReadLine();
                sh.Close();
            }
            //If could not add service endpoint(If binding is null or address is null )
            catch (ArgumentNullException)
            {
                Console.WriteLine("Error : The endpoint address is invalid ");
            }

            //If open() / close() method fails - service host object is not in a opened or opening and modifiable state
            //If service host object is in a closed/closing and not modifiable state  
            catch (InvalidOperationException)
            {
                Console.WriteLine("Error : Service host object is in either closed/closing/opened/opening state ");
            }

            //If open() / close() method fails - service host object is in a faulted state
            catch (CommunicationObjectFaultedException)
            {
                Console.WriteLine("Error : Service host object is corrupted ");
            }

            //If open() / close() method fails - If defualt time allocated for the operation exceeds
            catch (TimeoutException)
            {
                Console.WriteLine("Error : Biz tier program timed out !");
            }
        }
    }
}
