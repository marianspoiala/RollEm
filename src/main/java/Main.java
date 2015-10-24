import java.util.HashMap;
import java.util.Map;

public class Main {

    public static String FILE_PATH = "rollem.json";
    public static Map<String, UserDTO> userDTOs;

    /**
     * M-am gandit ca ar fi ok sa folosim HashMap pentru accesare usoara a userului selectat.
     * Daca nu e fisierul creat, va arunca o eroare initial, dar nu are importanta ca va trece mai departe, pentru ca e
     * catched. Am dat direct printStackTrace darputem afisa alt mesaj sau niciunul.
     * @param args
     */
    public static void main(String[] args) {
        System.out.println("Hello World!");

        GameService gameService = new GameServiceImpl();

        gameService.initGame();

        // Test pentru ca mi-e lene saa fac Unittest.
        for (Map.Entry<String, UserDTO> entry : userDTOs.entrySet()) {
            System.out.println(entry.getValue());
        }

        UserDTO userDTO = new UserDTO();
        userDTO.setName("ION");
        userDTO.setScore(100L);
        Main.userDTOs.put(userDTO.getName(), userDTO);

        gameService.saveGame();
    }
}
