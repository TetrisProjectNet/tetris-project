import { animate, keyframes, state, style, transition, trigger } from "@angular/animations";

export function getAnimations(isMobile: boolean) {

  const mobileValues = ['Y(-340px)', 'Y(20px)', 'Y(0)'];  
  const desktopValues = ['X(340px)', 'X(-20px)', 'X(0)'];
  let finalValues = [];

  isMobile ? finalValues = mobileValues : finalValues = desktopValues;
  
  return [
    trigger('flyInOut', [
      state('inactive', style({
        opacity: 1,
      })),
      transition('inactive <=> active', animate('500ms ease-out', keyframes([
        style({
          transform: `translate${finalValues[0]}`,
          offset:0,
          opacity: 0,
        }),
        style({
          offset:.7,
          opacity: 1,
          transform: `translate${finalValues[1]}`
        }),
        style({
          offset: 1,
          transform: `translate${finalValues[2]}`,
        })
      ]))),
      transition('active => removed', animate('500ms ease-in', keyframes([
        style({
          transform: `translate${finalValues[1]}`,
          opacity: 1,
          offset: .2
        }),
        style({
          opacity:0,
          transform: `translate${finalValues[0]}`,
          offset: 1
        })
      ])))
    ]),
  ];
}
