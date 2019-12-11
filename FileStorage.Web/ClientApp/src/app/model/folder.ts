export class Folder {
    constructor(
        public id: string,
        public folderName: string,
        public userId: string,
        public parentFolderId?: string
    ) { }
}