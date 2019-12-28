import { Guid } from 'guid-typescript';

// export class FileModel{
//     constructor(
//         public id : string,
//         public path : string,
//         public fileAccessibility : FileAccessibility,
//         public userId : string,
//         public categoryId : string,
//         public file : File,
//         public fileName : string,
//         public shortLink? : string,
//         public folderId? : string,
//         public categoryName? : string,
//         public folderName? : string,
//         public firstName? : string,
//         public lastName? : string
//     ){}
// }

export class FileModel {
    public id: string = Guid.EMPTY;
    public path: string;
    public fileAccessibility: FileAccessibility = FileAccessibility.Private;
    public userId: string;
    public categoryId: string;
    public file: File;
    public fileName: string;
    public shortLink: string;
    public folderId: string;
    public categoryName: string;
    public folderName: string;
    public firstName: string;
    public lastName: string;
}

export enum FileAccessibility {
    Private,
    Protected,
    Public
}