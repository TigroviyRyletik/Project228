ALTER TABLE reception
ADD CONSTRAINT FK_reception_Doctor
FOREIGN KEY (DoctorID)
REFERENCES Doctor (DoctorID)
ON DELETE NO ACTION -- або ON DELETE CASCADE, ON DELETE SET NULL, залежно від ваших потреб
ON UPDATE NO ACTION -- або ON UPDATE CASCADE, ON UPDATE SET NULL, залежно від ваших потреб;
GO