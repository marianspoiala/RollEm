import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.*;

/**
 * Created by Marian on 10/24/2015.
 */
public class GameServiceImpl implements GameService {
    public void initGame() {
        Path currentRelativePath = Paths.get("");
        String filePath = currentRelativePath.toAbsolutePath().toString();
        Main.FILE_PATH = filePath + "\\rollem.json";
        Main.userDTOs = new HashMap<String, UserDTO>();
        loadUsersFromFile();
    }

    public void saveGame() {
        saveUsersToFile();
    }

    private void saveUsersToFile() {
        JSONObject gameInfoJsonObj = new JSONObject();
        JSONArray usersJsonArray = new JSONArray();

        for (Map.Entry<String, UserDTO> entry : Main.userDTOs.entrySet()) {
            usersJsonArray.add(UserSerializer.toJSON(entry.getValue()));
        }

        gameInfoJsonObj.put("users", usersJsonArray);

        try {
            FileWriter file = new FileWriter(Main.FILE_PATH);
            file.write(gameInfoJsonObj.toJSONString());
            file.flush();
            file.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void loadUsersFromFile() {
        JSONParser parser = new JSONParser();

        try {
            Object obj = parser.parse(new FileReader(Main.FILE_PATH));

            JSONObject jsonObject = (JSONObject) obj;

            JSONArray usersJsonArray = (JSONArray) (((JSONObject)obj).get("users"));
            Iterator<JSONObject> iterator = usersJsonArray.iterator();
            UserDTO userDTO;
            while (iterator.hasNext()) {
                userDTO = UserSerializer.toDTO(iterator.next());
                Main.userDTOs.put(userDTO.getName(), userDTO);
            }
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (ParseException e) {
            e.printStackTrace();
        }
    }
}
