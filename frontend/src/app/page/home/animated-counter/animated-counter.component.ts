import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-animated-counter',
  templateUrl: './animated-counter.component.html',
  styleUrls: ['./animated-counter.component.scss']
})
export class AnimatedCounterComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  coutingLength: number = 1000;

  projectValue: number = 287;
  accurateValue: number = 95;
  clientValue: number = 300;
  customerValue: number = 100;

  //this is a variable that hold number
  projectCount: number = 0;
  //same process
  accurateCount: number = 0;
  clientCount: number = 0;
  customerfeedback: number = 0;

  //we have created setinterval function with arrow function and
  //and set them in a variable that is projectCountstop.
  projectCountstop: any = setInterval(()=>{
    this.projectCount++;
    //we need to stop this at  particular point; will use if condition
    if(this.projectCount == this.projectValue)
    {
      //clearinterval will stop tha function
      clearInterval(this.projectCountstop);
    }

  }, this.coutingLength / this.projectValue) //10 is milisecond you can control it


  accurateCountstop: any = setInterval(()=>{
    this.accurateCount++;
    if(this.accurateCount == this.accurateValue)
    {
      clearInterval(this.accurateCountstop);
    }
  }, this.coutingLength / this.accurateValue)


  clientCountstop: any = setInterval(()=>{
    this.clientCount++;
    if(this.clientCount == this.clientValue)
    {
      clearInterval(this.clientCountstop);
    }
  }, this.coutingLength / this.clientValue)

  customerfeedbackstop: any = setInterval(()=>{
    this.customerfeedback++;
    if(this.customerfeedback == this.customerValue)
    {
      clearInterval(this.customerfeedbackstop);
    }
  }, this.coutingLength / this.customerValue)

  //conclusion: we have use
  //string interpolation
  //setInterval function
  //()=> arrow function
  //clearInterval
  //creating variable


}
