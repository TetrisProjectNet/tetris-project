let emailObject = {
  generateTemplateWithCode: (code: string) => `<!DOCTYPE html>
  <html lang="en">
  <head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tetris-Project Verification Email</title>
  </head>
  <body>
  <div style="background-color: white; margin-top: 0px;">
    <table border="0" cellspacing="0" cellpadding="0" style="color: white; background-color: #212121; max-width: 600px; margin: 0 auto;">
      <thead>
        <tr>
          <th style="background-color: rgba(0, 0, 0, 0.3); width: 100%; padding: 30px 0px;">
            <h1 style="text-align: center; font-size: 36px; margin: 0; font-weight: bold;">TETRIS-PROJECT</h1>
          </th>
        </tr>
      </thead>

      <tbody>
        <tr>
          <td>
            <table>
              <thead>
                <tr>
                  <th style="padding: 25px 25px 0 25px;">
                    <h4 style="margin-top: 0; margin-bottom: 15px; text-align: start;">Forgot Password Verification</h4>
                  </th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td style="padding: 0 25px;">
                    <p style="margin: 0;">Your verification code:</p>
                  </td>
                </tr>
                <tr>
                  <td style="color: aqua; padding: 20px 25px;">
                    <h1 style="margin: 0; font-weight: bold;">${code}</h1>
                  </td>
                </tr>
                <tr>
                  <td style="padding: 0 25px;">
                    <p style="margin-bottom: 0;">The verification code will be valid for 30 minutes. Please do not share this code with anyone.
                    </p>
                  </td>
                </tr>
                <tr>
                  <td style="padding: 0 25px;">
                    <p>
                      If you run into any issues, please contact us:
                      <a style="color: white;" href="mailto:tetrisprojectnet@gmail.com">tetrisprojectnet@gmail.com</a>
                    </p>
                  </td>
                </tr>
                <tr>
                  <td style="padding: 0 25px;">
                    <p style="margin: 0;"><i>This is an automated message, please do not reply.</i></p>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
        </tr>
        <tr>
          <td>
            <table style="width: 100%;">
              <tr>
                <td style="padding: 35px 25px 20px 25px;">
                  <div style="height: 0; width: 100%; border-top: 1px solid darkcyan;"></div>
                </td>
              </tr>
              <tr>
                <td style="padding:0 25px;">
                  <small>
                    You have received this email as a registered user of
                    <a style="color: white"
                      href="https://github.com/TetrisProjectNet/tetris-project">Tetris-project</a>.
                  </small>
                </td>
              </tr>
              <tr>
                <td style="padding: 0 25px 25px;">
                  <small style="margin-bottom: 0px;">
                    For more information about how we process data, please see our
                    <a style="color: white" href="https://github.com/TetrisProjectNet/tetris-project">Privacy
                      Policy</a>.
                  </small>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </tbody>

      <tfoot style="background-color: rgba(0, 0, 0, 0.15);">
        <tr>
          <td style="padding: 25px 25px 0 25px; text-align: center;">
            <small style="margin-bottom: 5px; margin-top: 0px;">
              © 2023 Copyright:
              <a style="color: aqua; font-weight: bold;" href="https://github.com/TetrisProjectNet/tetris-project"
                target="_blank">Tetris-project</a><br>
            </small>
          </td>
        </tr>
        <tr>
          <td style="padding: 0 25px 25px 25px; text-align: center;">
            <small>
              <i>All Rights Reserved.</i>
            </small>
          </td>
        </tr>
      </tfoot>

    </table>
  </div>
</body>

  </html>`


//   generateTemplateWithCode: (code: string) => `<!DOCTYPE html>
//   <html lang="en">
//   <head>
//     <meta charset="UTF-8">
//     <meta http-equiv="X-UA-Compatible" content="IE=edge">
//     <meta name="viewport" content="width=device-width, initial-scale=1.0">
//     <title>Document</title>
//   </head>
//   <body style="background-color: white; display: flex; justify-content: center; color: white; width: 100%;">
//   <div style="background-color: #212121; max-width: 600px;">

//     <div style="background-color: rgba(0, 0, 0, 0.3); width: 100%; padding: 40px 0px;">
//       <h1 style="text-align: center; font-size: 36px; margin: 0; font-weight: bold">TETRIS-PROJECT</h1>
//     </div>
//     <div>
//       <div style="padding: 25px; padding-bottom: 0px;">
//         <h4 style="margin-top: 10px; margin-bottom: 15px;">Forgot Password Verification</h4>
//         <p>Your verification code:</p>
//         <h1 style="color: aqua; margin-top: 20px; margin-bottom: 20px;">${code}</h1>
//         <p>
//           The verification code will be valid for 30 minutes. Please do not share this code with anyone.
//         </p>
//         <p>
//           <i>This is an automated message, please do not reply.</i>
//         </p>
//         <p>
//           If you run into any issues, please let us know here:
//           <i style="color: white !important;">tetrisprojectnet@gmail.com</i>
//         </p>
//       </div>

//       <small>
//         <div style="padding: 25px; padding-top: 0px;">
//           <hr style="color: rgba(255, 0, 0, 0.6); margin-top: 40px; margin-bottom: 20px;">
//           <p style="margin-bottom: 10px;">
//             You have received this email as a registered user of
//             <a style="color: white" href="https://github.com/TetrisProjectNet/tetris-project">Tetris-project</a>.
//           </p>
//           <p style="margin-bottom: 0px;">
//             For more information about how we process data, please see our
//             <a style="color: white" href="https://github.com/TetrisProjectNet/tetris-project">Privacy policy</a>.
//           </p>
//         </div>

//         <div style="background-color: rgba(0, 0, 0, 0.15); padding: 25px; text-align: center;">
//           <p style="margin-bottom: 5px; margin-top: 0px;">
//             © 2023 Copyright:
//             <a style="color: aqua; font-weight: bold;" href="https://github.com/TetrisProjectNet/tetris-project" target="_blank">Tetris-project</a><br>
//           </p>
//           <i>All Rights Reserved.</i>
//         </div>

//       </small>
//     </div>

//   </div>
// </body>

//   </html>`



  // generateTemplateWithCode: (code: string) => `<!DOCTYPE html>
  // <html lang="en">
  // <head>
  //   <meta charset="UTF-8">
  //   <meta http-equiv="X-UA-Compatible" content="IE=edge">
  //   <meta name="viewport" content="width=device-width, initial-scale=1.0">
  //   <title>Document</title>
  // </head>
  // <body style="background-color: #212121; height: 700px;">
  // <div style="background-color: rgba(0, 0, 0, 0.3); width: 100%; padding: 40px; border-bottom: thin solid transparent; border-image: radial-gradient(ellipse at center, rgba(aqua, 0.9) 70%, rgba(0, 0, 0, 0) 100%); border-image-slice: 1;">
  // <h1 style="text-align: center; color: white; font-size: 36px;">TETRIS-PROJECT</h1>
  // </div>
  // <h2 style="margin-top: 50px; color: white;">Your verification code</h2>
  // <h1 style="color: aqua; margin-top: 30px;">code --- ${code} --- code</h1>
  // <p style="color: white; margin-top: 50px;">Lorem ipsum dolor sit, amet consectetur adipisicing elit. In illum, aperiam quaerat sint sed suscipit beatae aliquam officiis iure, debitis quae recusandae inventore repellendus eum aspernatur? Recusandae culpa tempora iure.</p>

  // </body>
  // </html>`
}

export default emailObject;