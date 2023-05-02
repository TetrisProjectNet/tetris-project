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

  projectCount: number = 0;
  accurateCount: number = 0;
  clientCount: number = 0;
  customerfeedback: number = 0;

  projectCountstop: any = setInterval(()=>{
    this.projectCount++;
    if(this.projectCount == this.projectValue)
    {
      clearInterval(this.projectCountstop);
    }

  }, this.coutingLength / this.projectValue)


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

}
