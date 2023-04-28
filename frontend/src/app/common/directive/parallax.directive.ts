import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appParallax]'
})
export class ParallaxDirective {

  // @Input('factor') set parallaxFactor(val: any) {
  //   this.factor = val ? val : 1;
  // }

  // private factor: number = 0;
  // true: boolean = true;

  // constructor(
  //   private elementRef: ElementRef,
  //   private renderer: Renderer2
  // ) { }

  // @HostListener('window:scroll')
  // onWindowScroll() {
  //   // console.log('top: ', this.calcTopThreshold());
  //   console.log(this.elementRef.nativeElement.getBoundingClientRect());
  //   // console.log(window.innerHeight);
  //   // this.calcTopThreshold() > 0
  //   if (this.elementRef.nativeElement.getBoundingClientRect().top < window.innerHeight) {
  //     this.renderer.setProperty(
  //       this.elementRef.nativeElement,
  //       'style',
  //       `transform: translateY(${this.getTranslation()}px)`
  //     );

  //   }
  // }

  // private getTranslation() {
  //   return window.scrollY * this.factor / 10;
  // }

  // private calcTopThreshold() {
  //   return (
  //     this.elementRef.nativeElement.getBoundingClientRect().top +
  //     this.elementRef.nativeElement.getBoundingClientRect().height
  //   )
  // }

  // private calcBottomThreshold() {
  //   return (
  //     this.elementRef.nativeElement.getBoundingClientRect().top +
  //     this.elementRef.nativeElement.getBoundingClientRect().height
  //   )
  // }

  @Input('ratio') parallaxRatio: number = 1;
  initialTop: number = 0;

  constructor(
    private eleRef: ElementRef
  ) {  }

  ngOnInit(): void {
    // console.log('asd', this.eleRef.nativeElement.getBoundingClientRect());
    // console.log('scroll', window.scrollY);
    // console.log('parent: ', this.eleRef.nativeElement.parentElement.getBoundingClientRect());
    // console.log('offsetTop: ', this.eleRef.nativeElement.parentElement.getBoundingClientRect());
    // console.log(this.checkVisible(this.eleRef.nativeElement));



    // this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top -
    // this.eleRef.nativeElement.parentElement.getBoundingClientRect().top;
    // this.initialTop = 0;

    setTimeout(() => {
      this.initialTop = this.eleRef.nativeElement.offsetTop +
        this.eleRef.nativeElement.parentElement.offsetTop * this.parallaxRatio;

      this.eleRef.nativeElement.style.top = this.initialTop + 'px';
      // console.log('parallax: ', this.eleRef.nativeElement.style.top);

    },1)

    // if (this.eleRef.nativeElement.parentElement.getBoundingClientRect().top < 0) {
    //   this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top; // 301.4375
    // }

    // if (!this.checkVisible(this.eleRef.nativeElement) || this.eleRef.nativeElement.parentElement.getBoundingClientRect().top > 0) {
    //   // this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top; // 301.4375
    //   // this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top -
    //   //   (this.eleRef.nativeElement.parentElement.getBoundingClientRect().bottom * this.parallaxRatio);
    // } else {
    //   // this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top; // 301.4375

    // }
    // if (this.checkVisible(this.eleRef.nativeElement)) {
    //   console.log('yes');
    //   this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top; // 301.4375
    // } else {
    //   this.initialTop = this.eleRef.nativeElement.getBoundingClientRect().top * this.parallaxRatio; // 301.4375
    //   console.log('no', this.initialTop);
    // }
  }

  ngAfterViewInit() {

  }


  @HostListener("window:scroll", ["$event"])
  onWindowScroll() {
    // console.log('scroll: ', window.scrollY);
    // console.log('TOP:', this.eleRef.nativeElement.style.top);
    // // if (window.scrollY > 1450) {
    // //   this.eleRef.nativeElement.style.top = (this.initialTop - (window.scrollY * this.parallaxRatio + window.scrollY)) / 10 + 'px';
    // // }
    // if (!this.checkVisible(this.eleRef.nativeElement)) {
    //   // this.eleRef.nativeElement.style.display = 'none';
    // } else {
    //   // this.eleRef.nativeElement.style.display = 'block';

    // }
    // console.log('asd');
    this.eleRef.nativeElement.style.top = (this.initialTop - (window.scrollY * this.parallaxRatio)) + 'px';
  }

  // checkVisible(elem: any) {
  //   var rect = elem.getBoundingClientRect();
  //   var viewHeight = Math.max(document.documentElement.clientHeight, window.innerHeight);
  //   return !(rect.bottom < 0 || rect.top - viewHeight >= 0);
  // }

}
