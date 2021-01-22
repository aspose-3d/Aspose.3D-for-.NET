import { Injectable, Inject } from '@angular/core';
import { ServicesModule, API_CONFIG } from './services.module';
import axios from 'axios';

@Injectable({
  providedIn: ServicesModule
})
export class FileService {

  public finalResult: boolean;

  constructor(@Inject(API_CONFIG) private uri: string) { }

  //Upload file
  upload(api: string, file: any) {
    return new Promise((res, rej) => {
      let params = new FormData()
      for (const key of Object.keys(file)) {
        params.append(key, file[key])
      }
      axios.post(this.uri + api, params, { headers: { 'Content-Type': 'multipart/form-data' } })
        .then(function (response) {
          res(response);
        })
        .catch(function (error) {
          rej(error);
        });
    });
  }

  //Get sessionId
  getSessionId(api: string, sessionId: any, folderName: any, filePathName: any) {
    return new Promise((res, rej) => {
      axios.get(this.uri + api, { params: { 'sessionId': sessionId, 'folderName': folderName, 'filePathName': filePathName } })
        .then(function (response) {
          res(response);
        })
        .catch(function (error) {
          rej(error);
        });
    });
  }

  //File download
  download(api: string, type: string, resultType: any, sessionId: any) {
    window.open(this.uri + api + sessionId + "?type=" + type + "&resultType=" + resultType);
  }

  // 3d conversion
  convertFile(sessionId: string, outputType: string) {
    const data = {
      sessionId: sessionId,
      outputType: outputType
    }
    return new Promise((res, rej) => {
      axios.post(this.uri + 'api/conversion/convertFile', data)
        .then(function (response) {
          res(response);
        })
        .catch(function (error) {
          rej(new Error("File conversion failed"));
        })
    });
  }

  //Conversion file download
  downloadconversion(api: string, sessionId: string) {
    window.open(this.uri + api + "/" + sessionId);
  }

  //Send mail
  sendEmail(api: string, address: string, url: string) {
    return new Promise((res, rej) => {
      axios.get(this.uri + api, { params: { 'address': address, 'url': url } })
        .then(function (response) {
          res(response);
        })
        .catch(function (error) {
          rej(error);
        });
    });
  }

  //Get sessionid algorithm
  creatUuid() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
      s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
  }

  //Determine file dependency
  judgeFile(files: any) {
    var suffixlist: any = [];
    if (files.length) {
      suffixlist = this.getFileSuffix(files);
      if (suffixlist.indexOf("gltf") > -1) {
        let file = files[suffixlist.indexOf("gltf")];
        let reader = new FileReader();
        reader.onload = () => {
          let str = JSON.stringify(reader.result);
          var obj = eval('(' + str + ')');
          obj = JSON.parse(obj);
          var Jstr = obj.buffers[0].uri;
          var Jres = Jstr.startsWith("data:application/octet-stream;base64");
          this.finalResult = Jres;
        }
        reader.readAsText(file);
        return this.finalResult;
      }
    }
  }

  //Gets the file suffix collection
  getFileSuffix(files: any) {
    var suffixlist: any = [];
    for (let i = 0; i < files.length; i++) {
      var name = files[i].name;
      var strArray = name.split(".");
      var suffixIndex = strArray.length - 1;
      var suffixName = strArray[suffixIndex];
      suffixlist.push(suffixName);
    }
    return suffixlist;
  }

  //Upload error report
  uploadErrorReport(email: string, session: string, application: string) {
    const data = {
      "email": email,
      "session": session,
      "application": application
    }
    return new Promise((res, rej) => {
      axios.post(this.uri + 'api/error2forum', data)
        .then(function (response) {
          res(response);
        })
        .catch(function (error) {
          rej(new Error("File conversion failed"));
        })
    });
  }

  //Judge whether 3D file exists
  judgeA3dw(api: string, sessionId: any) {
    return new Promise((res, rej) => {
      axios.get(this.uri + api + sessionId)
        .then(function (response) {
          res(response);
        })
        .catch(function (error) {
          rej(error);
        });
    });
  }
}
