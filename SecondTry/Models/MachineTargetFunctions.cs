using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondTry.Models
{
    /*
        Takes the user and the database connection as constructor values
        and contains functions to calculate and return Results and Target
        for OEE, Waste and stop.
    */

    public class MachineTargetFunctions
    {
        private dbContext context;
        private loggedInUser user;
        public ProdShift prodShift;

        public MachineTargetFunctions(dbContext context, loggedInUser user) {
            this.context = context;
            this.user = user;
            prodShift = (from p in context.ProdShift where p.Machine == user.maskin && p.ShiftNo == user.selShift.shiftno select p).FirstOrDefault();
        }
        
        private double TargetTime()
        {
            double shiftSumTargetTime = prodShift.ShiftSumTargetTime;
            double ProdHourNet = prodShift.ProdHourNet;
            if (ProdHourNet * 60 < shiftSumTargetTime)
            {
                return ProdHourNet * 60;
            }
            else {
                return shiftSumTargetTime;
            }
        }
        
        // Returns a double containing current stop Result
        public double funcStopResult(int numDec) {
            double ProdHourNet = prodShift.ProdHourNet;
            int StopsUnPlanned = prodShift.StopsUnPlanned;

            if (ProdHourNet == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(StopsUnPlanned*(8/ProdHourNet), numDec); 
            }
        }

        // Returns a double containing stop target
        public double funcStopTarget() {
            double st8h = prodShift.stop8h;
            int extraStop = prodShift.ExtraStop;
            return st8h + extraStop;
             
        }

        // Returns a double containing current waste Result
        public double funcWasteResult(int numDec) {
            double waste = (double)(prodShift.Wastetot);
            double approvedQty =(double) prodShift.ApprovedQTY;

            if (waste + approvedQty == 0)
            {
                return 0;
            }
            else {
                return Math.Round((waste / (approvedQty + waste))*100, numDec);
            }
        }

        //Return a double containing current waste target
        public double funcWasteTarget() {
            double wasteTarget = prodShift.WasteTarget;
            double changeOverExtraWaste = prodShift.ChangeOverExtraWaste;
            double uhExtraWaste = prodShift.UHExtraWaste;
            double prodHourNet = prodShift.ProdHourNet;

            if (prodHourNet == 0)
            {
                return 0;
            }
            else {
                return Math.Round(wasteTarget + (changeOverExtraWaste + uhExtraWaste)*(8/prodHourNet), 1);
            }
        }

        // Returns a double containing current OEE Result
        public double funcOeeResult(int numDec) {
            int approvedQty = prodShift.ApprovedQTY;
            double prodHourNet = prodShift.ProdHourNet;
            double budMachSpeedValue = prodShift.BudMachSpeed;

            if (prodHourNet == 0 || budMachSpeedValue == 0)
            {
                return 0;
            }
            else {
                return Math.Round(((approvedQty / (prodHourNet*60))/budMachSpeedValue)*100, numDec);
            }
        }
        
        // Returns a double containing OEE target
        public double funcOeeTarget()
        {
            double oee = prodShift.OEETarget;
            double targetTime = TargetTime();
            double uh = prodShift.UHHour;
            double prodHourNet = prodShift.ProdHourNet;
            double budMachSpeedValue = prodShift.BudMachSpeed;

            if (prodHourNet == 0 || budMachSpeedValue == 0)
            {
                return 0;
            }
            else if ((int)(oee * (1 - ((uh * 60 + targetTime) / 60) / prodHourNet)) + 0.99 < 0)
            {
                return 0;
            }
            else {
                return Math.Ceiling((oee * (1 - ((uh * 60 + targetTime) / 60) / prodHourNet)));
            }
        }
        /*
         
            Calculates all Targets and Results and returns a
            machineTargetResult.
        */
        public machineTargetResult calcTargetResult() {
            machineTargetResult temp = new machineTargetResult();
            double oeeResult = funcOeeResult(1);
            double stopResult = funcStopResult(1);
            double wasteResult = funcWasteResult(1);

            temp.oeeResult = String.Format("{0:0.0}",oeeResult);
            temp.wasteResult = String.Format("{0:0.0}", wasteResult);
            temp.stopResult = stopResult.ToString();

            temp.oeeTarget = funcOeeTarget();
            temp.wasteTarget = funcWasteTarget();
            temp.stopTarget = funcStopTarget();

            if (oeeResult >= funcOeeTarget())
            {
                temp.oeeColor = "green";
            }
            else {
                temp.oeeColor = "red";
            }

            if (stopResult <= funcStopTarget())
            {
                temp.stopColor = "green";
            }
            else
            {
                temp.stopColor = "red";
            }

            if (wasteResult < funcWasteTarget())
            {
                temp.wasteColor = "green";
            }
            else
            {
                temp.wasteColor = "red";
            }

            return temp;
        }
    }   
}