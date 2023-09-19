
 # Prerequisites
Before you begin, ensure you have the following installed:

.NET 6
Docker (optional)

 # Getting Started
Building the API
Clone this repository to your local machine:
git clone https://github.com/malekmsallem/powerplant-api.git
Navigate to the project directory:


cd PowerPlanApi
Build the API:
dotnet build

Running the API Locally
To run the API locally, use the following command:

dotnet run --project PowerPlanApi
The API will be accessible at http://localhost:8888/swagger.

Running the API as a Docker Container (Optional)
Build the Docker image:

docker build -t PowerPlanApi .
Run the Docker container:

docker run -p 8888:80 PowerPlanApi
The API will be accessible at http://localhost:8888.