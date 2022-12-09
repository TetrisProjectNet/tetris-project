export class User {
  // ----- ALAPVETŐ -----
  _id: string = '';
  // firstName: string = '';  - nem hiszem, hogy kell
  // lastName: string = '';  - nem hiszem, hogy kell
  username: string = '';
  email: string = '';
  password: string = '';
  role: string = '';  //  player / admin
  banned: boolean = false;  //  ha szeretnénk banolási lehetőséget

  // ----- STATISZTIKÁKHOZ -----
  joinDate: string = '';
  scores?: string[] = [];

  // ----- SHOP -----
  coins: number = 0;  //  lehet default 100 mondjuk
  shopItems?: number[] = [];  //  megvásárolt termékek id-jeinek tömbje

  //  ha szeretnénk 1v1-et, vagy barátok listáját megjeleníteni (ez szerintem elég lehet csak weben)
  friends?: string[] = [];  //  barátok id-jeinek tömbje

  //  ha szeretnénk visszajátszási lehetőséget
  games?: number[] = [];  //  játékok id-jeinek tömbje
}
