import { ShopItem } from "./shop-item";

export class User {
  // ----- ALAPVETŐ -----
  id: number = 0;
  username: string = '';
  email: string = '';
  password: string = '';
  role: string = '';  //  player / admin
  banned: boolean = false;  //  ha szeretnénk banolási lehetőséget

  // ----- STATISZTIKÁKHOZ -----
  joinDate: string = '';
  lastOnlineDate: string = '';
  scores?: number[] = [];

  // ----- SHOP -----
  coins: number | string = '';  //  lehet default 100 mondjuk
  shopItems?: ShopItem[] = [];  //  megvásárolt termékek id-jeinek tömbje

  //  ha szeretnénk 1v1-et, vagy barátok listáját megjeleníteni (ez szerintem elég lehet csak weben)
  friends?: User[] = [];  //  barátok id-jeinek tömbje

  //  ha szeretnénk visszajátszási lehetőséget
  // games?: number[] = [];  //  játékok id-jeinek tömbje
}
