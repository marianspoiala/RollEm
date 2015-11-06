using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface GameUtilsIF
{
    void saveUsersDetails(List<UserDTO> users, String fileName);

    List<UserDTO> loadUsersDetails(String fileName);

    void startTimer(Double seconds);
}
