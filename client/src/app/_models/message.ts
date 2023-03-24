export interface Message {
  id: number
  senderId: number
  senderUsername: string
  senderPhotoUrl: string
  recipientId: number
  recipientUsername: string
  recipientPhotoUrl: string
  content: string
  dataRead?: Date
  messageSent: Date
}


// thuwongg đc định nghĩa các lớp trong đói tượng đc sử dụng
