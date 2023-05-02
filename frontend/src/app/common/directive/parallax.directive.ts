import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appParallax]'
})
export class ParallaxDirective {

  @Input('ratio') parallaxRatio: number = 1;
  initialTop: number = 0;

  constructor(
    private eleRef: ElementRef
  ) {  }

  ngOnInit(): void {
    setTimeout(() => {
      this.initialTop = this.eleRef.nativeElement.offsetTop +
        this.eleRef.nativeElement.parentElement.offsetTop * this.parallaxRatio;

      this.eleRef.nativeElement.style.top = this.initialTop + 'px';
    },1)
  }

  @HostListener("window:scroll", ["$event"])
  onWindowScroll() {
    this.eleRef.nativeElement.style.top = (this.initialTop - (window.scrollY * this.parallaxRatio)) + 'px';
  }

}
