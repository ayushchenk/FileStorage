import { Guid } from 'guid-typescript';

// export class Category{
//     constructor(
//         public id : string,
//         public categoryName : string
//     ){}
// }

export class Category {
    public id: string = Guid.EMPTY;
    public categoryName: string;
}