export class FileModel{
    constructor(
        public id : string,
        public path : string,
        public fileAccessibility : FileAccessibility,
        public userId : string,
        public categoryId : string,
        public file : File,
        public fileName : string,
        public shortLink? : string,
        public folderId? : string,
        public categoryName? : string,
        public folderName? : string,
        public firstName? : string,
        public lastName? : string
    ){}
}

export enum FileAccessibility {
    Private, 
    Protected, 
    Public
  }