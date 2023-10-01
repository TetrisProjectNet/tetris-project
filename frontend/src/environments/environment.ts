// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  // apiUrl: 'http://localhost:3000/'
  // apiUrl: 'http://192.168.50.17:3000/'
  // apiUrl: 'http://192.168.1.101:3000/'
  apiUrl: 'https://tetris.kando-dev.eu/',
  firebaseConfig: {
    apiKey: "AIzaSyBuo77yHfkMPOzYfePtNQfOkRDJvNS0QWc",
    authDomain: "tetris-project-net-1.firebaseapp.com",
    projectId: "tetris-project-net-1",
    storageBucket: "tetris-project-net-1.appspot.com",
    messagingSenderId: "498840282506",
    appId: "1:498840282506:web:b75f7ea13b9b63118fd473"
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
