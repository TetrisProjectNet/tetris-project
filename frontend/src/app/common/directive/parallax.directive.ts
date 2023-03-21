import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appParallax]'
})
export class ParallaxDirective {

  @Input('factor') set parallaxFactor(val: any) {
    this.factor = val ? val : 1;
  }

  private factor: number = 0;
  true: boolean = true;

  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2
  ) { }

  @HostListener('window:scroll')
  onWindowScroll() {
    // console.log('top: ', this.calcTopThreshold());
    console.log(this.elementRef.nativeElement.getBoundingClientRect());
    // console.log(window.innerHeight);
    // this.calcTopThreshold() > 0
    if (this.elementRef.nativeElement.getBoundingClientRect().top < window.innerHeight) {
      this.renderer.setProperty(
        this.elementRef.nativeElement, 
        'style',
        `transform: translateY(${this.getTranslation()}px)`
      );

    }
  }

  private getTranslation() {
    return window.scrollY * this.factor / 10;
  }

  private calcTopThreshold() {
    return (
      this.elementRef.nativeElement.getBoundingClientRect().top + 
      this.elementRef.nativeElement.getBoundingClientRect().height
    )
  }

  private calcBottomThreshold() {
    return (
      this.elementRef.nativeElement.getBoundingClientRect().top + 
      this.elementRef.nativeElement.getBoundingClientRect().height
    )
  }
}
