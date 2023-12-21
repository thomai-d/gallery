import { Outlet, RouterProvider, createBrowserRouter } from "react-router-dom";
import { GalleryOverviewPage } from "./pages/gallery-overview/gallery-overview.page";
import { ErrorPage } from "./pages/error/error-page.component";
import { GalleryDetailPage } from "./pages/gallery-detail/gallery-detail.page";
import { QueryClient, QueryClientProvider } from "react-query";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Outlet />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "",
        element: <GalleryOverviewPage />
      },
      {
        path: "gallery/:id",
        element: <GalleryDetailPage />,
      },
    ]
  }
]);

const queryClient = new QueryClient()

export const App = () => {
  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router} />
    </QueryClientProvider>
  );
}