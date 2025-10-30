# CarWatch

An app where citizens can report illegally parked vehicles so authorities can verify and take action. 

## Technology Stack

<img align="left" alt="Dotnet" width="50px" style="padding-right:10px;" src="https://github.com/devicons/devicon/blob/v2.17.0/icons/dot-net/dot-net-plain-wordmark.svg"/>
<img align="left" alt="Dotnet" width="50px" style="padding-right:10px;" src="https://github.com/devicons/devicon/blob/v2.17.0/icons/react/react-original.svg"/>
<img align="left" alt="Dotnet" width="50px" style="padding-right:10px;" src="https://github.com/devicons/devicon/blob/v2.17.0/icons/postgresql/postgresql-original.svg"/>

<br>
<br>

## Run instructions

In order to run this project please follow these steps:

### 1. Install Docker and/or DokerDesktop :

<a href="https://docs.docker.com/desktop/setup/install/windows-install/">
  <img alt="Windows" width="50px" style="padding-right:100px;" src="https://github.com/devicons/devicon/blob/v2.17.0/icons/windows8/windows8-original.svg"/>
</a>
<br>
<a href="https://docs.docker.com/desktop/setup/install/linux/">
  <img alt="Linux" width="50px" style="padding-right:100px;" src="https://github.com/devicons/devicon/blob/v2.17.0/icons/linux/linux-original.svg"/>
</a>
<br>
<a href="https://docs.docker.com/desktop/setup/install/mac-install/">
  <img alt="MacOS" width="50px" style="padding-right:100px;" src="https://github.com/devicons/devicon/blob/v2.17.0/icons/apple/apple-original.svg"/>
</a>

<br>
<br>

### 2. Install .NET SDK 9

[https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)

### 3. Install Node.js v24.11

[https://nodejs.org/en/download](https://nodejs.org/en/download)

### 4. Run project

#### 4.1 Run Backend

```bash
git clone "https://github.com/Balcus/CarWatch.git"
cd CarWatch/AppHost
dotnet restore
dotnet run
```

If there are no build errors, this should output the path used to log into the Aspire Dashboard

#### 4.2 Run Frontend

```bash
cd CarWatch/frontend
npm install
npm start
```

If there are no build errors, this should output the path to the frontend web page 